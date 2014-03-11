using System;
using System.Collections.Generic;
using System.Linq;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using Type = CppSharp.AST.Type;

namespace FFmpegBindings.Passes
{
    /// <summary>
    ///     Some kinds of fields are not supported in C# pinvoke, in particular fixed arrays can only be of a primitive type.
    /// </summary>
    public class UnwrapUnsupportedArraysPass : TranslationUnitPass
    {
        public override bool VisitClassDecl(Class @class)
        {
            var toRemove = new List<Field>();
            var toAdd = new List<Tuple<int, Field>>();
            for (int index = 0; index < @class.Fields.Count; index++)
            {
                Field field = @class.Fields[index];
                int fieldIndex = index;
                List<Field> unwrapped = TryUnwrapField(@class, field).ToList();
                if (unwrapped.Any())
                    toRemove.Add(field);
                toAdd.AddRange(unwrapped.Select(v => Tuple.Create(fieldIndex, v)));
            }
            toAdd.Reverse();
            foreach (var field in toAdd)
            {
                @class.Fields.Insert(field.Item1, field.Item2);
            }

            foreach (Field field in toRemove)
            {
                @class.Fields.Remove(field);
            }
            return true;
        }

        private IEnumerable<Field> TryUnwrapField(Class @class, Field field)
        {
            // check that field is a constant size array
            var arrayType = field.Type as ArrayType;
            if (arrayType != null && arrayType.SizeType == ArrayType.ArraySize.Constant)
            {
                Type typeInArray = arrayType.Type.Desugar();

                if (IsSupportedArray(typeInArray)) yield break;

                UnwrapInfo? unwrapInfo = GetUnwrapInfo(arrayType, typeInArray);

                if (unwrapInfo == null)
                {
                    Driver.Diagnostics.EmitWarning("Failed to unwrap {0}.{1} ({2})",
                        @class.Name, field.Name, field.Type);
                    yield break;
                }

                Driver.Diagnostics.EmitMessage("Unwrapping {0}.{1} ({2})",
                    @class.Name, field.Name, field.Type);

                for (int i = 0; i < unwrapInfo.Value.UnwrapCount; i++)
                {
                    // field[N] becomes field_0 .. field_N
                    string unwrappedName = field.Name + "_" + i;

                    var unwrappedField = new Field(field)
                    {
                        Name = unwrappedName,
                        Offset = (uint) (field.Offset + i*unwrapInfo.Value.UnwrapTypeWidth),
                        QualifiedType = new QualifiedType(unwrapInfo.Value.UnwrapType, field.QualifiedType.Qualifiers),
                    };

                    IEnumerable<Field> unwrappedAgain = TryUnwrapField(@class, unwrappedField).ToList();
                    if (unwrappedAgain.Any())
                    {
                        foreach (Field field1 in unwrappedAgain)
                        {
                            yield return field1;
                        }
                    }
                    else
                    {
                        yield return unwrappedField;
                    }
                }
            }
        }

        private static bool IsSupportedArray(Type typeInArray)
        {
            if (typeInArray.IsPrimitiveType())
            {
                // no need to unwrap, as it is legal to have fixed arrays of primitive types (in C# pinvoke)
                return true;
            }
            return false;
        }

        private UnwrapInfo? GetUnwrapInfo(ArrayType arrayType, Type typeInArray)
        {
            long unwrapCount;
            Type unwrapType;
            // in bits
            long unwrapTypeWidth;
            if (typeInArray.IsPointerToPrimitiveType())
            {
                // need to unwrap if field is array of pointer to primitive type
                // int* A[N] 
                // becomes
                // int* A_0;
                // int* A_N;
                // unwrap to multiple pointer fields
                unwrapType = new PointerType
                {
                    QualifiedPointee = ((PointerType) typeInArray).QualifiedPointee
                };
                unwrapCount = arrayType.Size;
                // TODO: get type width from driver TargetInfo!
                unwrapTypeWidth = 0;
            }
            else if (typeInArray.IsPointerToArrayType())
            {
                // need to unwrap if field is array of pointer to array
                // A (*int[N])[M]
                // becomes
                // int** X_0;
                // int** X_M;
                var innerArray = (ArrayType) ((PointerType) typeInArray).Pointee;

                unwrapType = new PointerType
                {
                    QualifiedPointee = new QualifiedType(new PointerType
                    {
                        QualifiedPointee = new QualifiedType(innerArray.Type)
                    })
                };
                unwrapCount = arrayType.Size;
                unwrapTypeWidth = Driver.TargetInfo.PointerWidth;
            }
            else if (typeInArray is ArrayType)
            {
                // need to unwrap if field is array of array
                // int A[N][M]
                // becomes
                // int A_0[M];
                // int A_N[M];
                Type innerArray = ((ArrayType) (typeInArray)).Type;
                unwrapType = new ArrayType
                {
                    Size = ((ArrayType) (typeInArray)).Size,
                    SizeType = ArrayType.ArraySize.Constant,
                    Type = innerArray
                };
                unwrapCount = arrayType.Size/((ArrayType) (typeInArray)).Size;
                // TODO: get type width from driver TargetInfo!
                unwrapTypeWidth = 0;
            }
            else if (!typeInArray.IsPrimitiveType())
            {
                // need tp unwrap if field is array of complex type
                // Struct A[N]
                // becomes
                // Struct A_0;
                // Struct A_N;
                unwrapType = typeInArray;
                unwrapCount = arrayType.Size;
                // TODO: get type width from driver TargetInfo!
                unwrapTypeWidth = 0;
            }
            else
            {
                return null;
            }

            return new UnwrapInfo
            {
                UnwrapType = unwrapType,
                UnwrapCount = unwrapCount,
                UnwrapTypeWidth = unwrapTypeWidth
            };
        }

        private struct UnwrapInfo
        {
            public Type UnwrapType { get; set; }
            // in bits!
            public long UnwrapTypeWidth { get; set; }
            public long UnwrapCount { get; set; }
        }
    }
}
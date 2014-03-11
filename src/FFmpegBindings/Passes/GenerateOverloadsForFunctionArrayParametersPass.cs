using System;
using System.Collections.Generic;
using System.Linq;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using Type = CppSharp.AST.Type;

namespace FFmpegBindings.Passes
{
    public class GenerateOverloadsForFunctionArrayParametersPass : ArrayTypeUnwrapperBase
    {
        private readonly DeclarationContext _wrapperClassContext;

        public GenerateOverloadsForFunctionArrayParametersPass(DeclarationContext wrapperClassContext)
        {
            _wrapperClassContext = wrapperClassContext;
        }

        public override bool VisitDeclarationContext(DeclarationContext context)
        {
            var toInsert = new List<Tuple<int, Function>>();
            for (int i = 0; i < context.Functions.Count; i++)
            {
                Function function = context.Functions[i];

                if (function.Parameters.Any(p => ShouldUnwrapParameter(p.Type)))
                {
                    var clone = new Function(function);
                    ProcessFunction(clone, _wrapperClassContext);
                    toInsert.Add(Tuple.Create(i, clone));
                }
            }
//            foreach (var tuple in toInsert)
//            {
//                context.Functions.Insert(tuple.Item1, tuple.Item2);
//            }
            return true;
        }

        private void ProcessFunction(Function clone, DeclarationContext ctx)
        {
            for (int index = 0; index < clone.Parameters.Count; index++)
            {
                Parameter parameter = clone.Parameters[index];

                Type oldType = parameter.Type;
                ArrayType arrayType;
                Type typeInArray;
                if (!ShouldUnwrapParameter(oldType, out arrayType, out typeInArray))
                {
                    continue;
                }

                clone.Parameters[index] = parameter = new Parameter(parameter);

                UnwrapInfo? unwrapInfo = GetUnwrapInfo(arrayType, typeInArray, true);
                // we should not get null, since the ShouldUnwrapField check above should have 
                if (unwrapInfo == null)
                {
                    Driver.Diagnostics.EmitWarning("Failed to handle const-size array field {0}.{1} of type {2}",
                        clone.Name, parameter.Name, oldType);
                    continue;
                }

                // generate wrapper class types in class namespace (for reuse)
                ArrayWrapperClass wrapperClass = GetCreateWrapperStruct(ctx, unwrapInfo.Value);

                parameter.QualifiedType = new QualifiedType(new TagType(wrapperClass));
            }
        }

        protected static bool ShouldUnwrapParameter(Type type)
        {
            ArrayType arrayType;
            Type typeInArray;
            return ShouldUnwrapField(type, out arrayType, out typeInArray);
        }

        protected static bool ShouldUnwrapParameter(Type type, out ArrayType arrayType, out Type typeInArray)
        {
            // check that field is a constant size array
            if (IsConstSizeArray(type, out arrayType))
            {
                typeInArray = arrayType.Type.Desugar();
                return true;
            }
            typeInArray = null;
            return false;
        }
    }
}
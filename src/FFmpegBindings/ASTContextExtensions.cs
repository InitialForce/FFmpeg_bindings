using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CppSharp.AST;
using Type = CppSharp.AST.Type;

namespace CppSharp
{
    internal static class ASTContextExtensions
    {
        public static void IgnoreFunctionsWithParameterTypeName(this ASTContext context, string matchTypeName)
        {
            foreach (TranslationUnit unit in context.TranslationUnits)
            {
                foreach (Function function in unit.Functions)
                {
                    if (function.Parameters.Any(
                        p =>
                        {
                            Type type = p.Type;
                            // depointer
                            if (type.IsPointer())
                            {
                                type = ((PointerType)type).Pointee;
                            }
                            string typeName;
                            if (type is TypedefType)
                            {
                                typeName = ((TypedefType)type).Declaration.Name;
                            }
                            else
                            {
                                typeName = type.ToString();
                            }
                            bool match = typeName == matchTypeName;
                            return match;
                        }))
                    {
                        function.ExplicityIgnored = true;
                    }
                }
            }
        }

        /// <summary>
        /// Convert size_t to UIntPtr (pointer sized uint)
        /// </summary>
        /// <param name="context"></param>
        public static void ConvertTypesToPortable(this ASTContext context, Func<TypedefType, bool> matchFunc, PrimitiveType primitiveType)
        {
            foreach (var tu in context.TranslationUnits)
            {
                var sizeT = tu.FindTypedef("size_t", false);
                if (sizeT != null)
                {
                    ModifyPrimitiveTypeDef(sizeT.Type, matchFunc, primitiveType);
                }
            }

            foreach (var tu in context.TranslationUnits)
            {
                foreach (var func in tu.Functions)
                {
                    foreach (var param in func.Parameters)
                    {
                        ModifyPrimitiveTypeDef(param.Type, matchFunc, primitiveType);
                    }

                }

                foreach (var @class in tu.Classes)
                {
                    foreach (var field in @class.Fields)
                    {
                        ModifyPrimitiveTypeDef(field.Type, matchFunc, primitiveType);
                    }

                }
            }
        }

        private static void ModifyPrimitiveTypeDef(Type type, Func<TypedefType, bool> matchFunc, PrimitiveType primitiveType)
        {
            var typedefType = type as TypedefType;
            if (typedefType != null)
            {
                if (matchFunc(typedefType))
                {
                    if ((typedefType.Declaration.QualifiedType.Type is BuiltinType) &&
                        ((BuiltinType)typedefType.Declaration.QualifiedType.Type).Type == primitiveType)
                        return;

                    typedefType.Declaration.QualifiedType =
                        new QualifiedType(new BuiltinType(primitiveType),
                            typedefType.Declaration.QualifiedType.Qualifiers);
                }
            }
        }

        public static void ChooseAndPromoteIncompleteClass(this ASTContext context)
        {
            // get all classes across all units
            var allClasses = context.TranslationUnits.Aggregate(Enumerable.Empty<Class>(), (p, u) => p.Concat(u.Classes));

            // group classes by name, select only duplicates
            var groupBy = allClasses.GroupBy(c => c.Name);
            var duplicates = groupBy.Select(c => c.ToList());

            foreach (var classGroup in duplicates)
            {
                // if there are no complete declarations
                if (classGroup.All(c => c.IsIncomplete))
                {
                    // promote first incomplete decl to be complete
                    // TODO: only remove the "correct" incomplete decl (perhaps use closest stringmatch to libname?)
                    var promotedClass = classGroup.First();
                    promotedClass.IsGenerated = true;
                    promotedClass.IsIncomplete = false;
                }
            }
        }

        public static void ResolveUnifyIncompleteClassDeclarations(this ASTContext context)
        {
            // get all classes across all units
            var allClasses = context.TranslationUnits.Aggregate(Enumerable.Empty<Class>(), (p, u) => p.Concat(u.Classes));

            // group classes by name, select only duplicates
            var duplicates = allClasses.GroupBy(c => c.Name).Select(c => c.ToList()).Where(c => c.Count() > 1).ToList();

            foreach (var group in duplicates)
            {
                var incompletes = @group.Where(c => c.IsIncomplete).ToList();
                var completes = @group.Where(c => !c.IsIncomplete).ToList();

                return;
                // if there is a complete declaration keep that one and ignore the incomplete ones
                if (completes.Count == 1)
                {
                    foreach (var @class in incompletes)
                    {
                        @class.IsGenerated = false;
                    }
                }
                else
                {
                    // ignore all but first incomplete decl
                    // TODO: only remove the "correct" incomplete decl (perhaps use closest stringmatch to libname?)
                    var promotedClass = @group.First();
                    promotedClass.IsGenerated = true;
                    promotedClass.IsIncomplete = false;
                    foreach (var @class in group.Skip(1))
                    {
                        @class.IsGenerated = false;
                    }
                }
            }
        }

        public static void ResolveUnifyIncompleteClassDeclarationsFromSubLibs(this ASTContext context, Driver[] dependentLibraryDrivers)
        {
            // get all classes across all units
            var allClasses = context.TranslationUnits.Aggregate(Enumerable.Empty<Class>(), (p, u) => p.Concat(u.Classes));

            // group classes by name, select only duplicates
            var duplicates = allClasses.GroupBy(c => c.Name);

            foreach (var group in duplicates)
            {
                if (@group.All(c => c.IsIncomplete || !c.IsGenerated))
                {
                    foreach (var dependentLibraryDriver in dependentLibraryDrivers)
                    {
                        var depLibClasses =
                            dependentLibraryDriver.ASTContext.TranslationUnits.Aggregate(Enumerable.Empty<Class>(),
                                (p, u) => p.Concat(u.Classes));
                        var matchClasses = depLibClasses.Where(c => c.Name == @group.Key);
                        if (matchClasses.Any())
                        {
                            // we found a complete class in a sublibrary, let's not generate this class ourselves..
                            foreach (var @class in group)
                            {
                                @class.IsGenerated = false;
                            }
                        }
                    }
                }
            }
        }
        //        private static IEnumerable<Class> CollectClasses(Class c)
        //        {
        //            if (c.Classes.Any())
        //                return Enumerable.Empty
        //
        //            return c.Classes.Aggregate(Enumerable.Empty<Class>(), (l, u) =>
        //            {
        //                if(c.Classes.Any())
        //                    return l.Concat(CollectClasses(c));
        //
        //                return l;
        //            });
        //        }
        //
        //        private static IEnumerable<Class> CollectClasses(TranslationUnit 
        //            tu)
        //        {
        //            return tu.Classes.Aggregate(Enumerable.Empty<Class>(), (l, c) => l.Concat(CollectClasses(c)));
        //        }
        //        
        //        public static void ResolveUnifyIncompleteClassDeclarationsFromSubLibs(this ASTContext context, IEnumerable<Driver> dependentLibraryDrivers)
        //        {
        //            // get all classes across all units
        //            var allClasses = context.TranslationUnits.Aggregate(Enumerable.Empty<Class>(),
        //                (l, tu) => l.Concat(CollectClasses(tu)));
        //
        //            // group classes by name, select only duplicates
        //            var duplicates = allClasses.GroupBy(c => c.Name);
        //
        //            foreach (var group in duplicates)
        //            {
        //                if (@group.All(c => c.IsIncomplete || !c.IsGenerated))
        //                {
        //                    foreach (var dependentLibraryDriver in dependentLibraryDrivers)
        //                    {
        //                        var depLibClasses =
        //                            dependentLibraryDriver.ASTContext.FindClass(@group.Key);
        //                        var matchClasses = depLibClasses.Where(c => c.Name == @group.Key);
        //                        if (matchClasses.Any())
        //                        {
        //                            if (@group.Key == "AVClass")
        //                                Debugger.Break();
        //
        //                            // we found a complete class in a sublibrary, let's not generate this class ourselves..
        //                            foreach (var @class in group)
        //                            {
        //                                @class.IsGenerated = false;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
    }
}
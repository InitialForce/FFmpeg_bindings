using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CppSharp;
using CppSharp.AST;

namespace FFmpegBindings
{
    public static class Extensions
    {
        public static Field GenerateConstValueFromMacro(this ASTContext context,
            MacroExpansion macro)
        {
            if (macro.Text != null && macro.Text.Contains("M_PI"))
                Debugger.Break();

            PrimitiveTypeExpression builtinTypeExpression = PrimitiveTypeExpression.TryCreate(macro.Text);
            if (builtinTypeExpression == null)
                return null;
            var valueType = new QualifiedType(new BuiltinType(builtinTypeExpression.Type))
            {
                Qualifiers = new TypeQualifiers {IsConst = true}
            };
            var item = new Field
            {
                Name = macro.Name,
                DebugText = macro.DebugText,
                Access = AccessSpecifier.Public,
                Expression =
                    builtinTypeExpression,
                QualifiedType = valueType
            };

            return item;
        }

        public static Field GenerateConstValueFromMacro(this ASTContext context,
            MacroDefinition macro)
        {
            PrimitiveTypeExpression builtinTypeExpression = PrimitiveTypeExpression.TryCreate(macro.Expression);
            if (builtinTypeExpression == null)
                return null;
            var valueType = new QualifiedType(new BuiltinType(builtinTypeExpression.Type))
            {
                Qualifiers = new TypeQualifiers {IsConst = true}
            };
            var item = new Field
            {
                Name = macro.Name,
                DebugText = macro.DebugText,
                Access = AccessSpecifier.Public,
                Expression =
                    builtinTypeExpression,
                QualifiedType = valueType
            };

            return item;
        }

        public static void CreateOverloadsForFunctionWithParamConstChar(this ASTContext context,
            IEnumerable<TranslationUnit> ourTranslationUnits)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
            }
        }

        public static void GenerateClassWithConstValuesFromMacros(this ASTContext context,
            IEnumerable<TranslationUnit> ourTranslationUnits, string className)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
                Class wrappingClass;
                if (GetCreateWrappingClass(className, tu, out wrappingClass))
                {
                    tu.Classes.Add(wrappingClass);
                }
                foreach (
                    MacroExpansion macro in
                        tu.PreprocessedEntities.OfType<MacroExpansion>())
                {
                    Field item = GenerateConstValueFromMacro(context, macro);
                    if (item == null)
                        continue;
                    wrappingClass.Fields.Add(item);
                }
                foreach (
                    MacroDefinition macro in
                        tu.PreprocessedEntities.OfType<MacroDefinition>())
                {
                    if (macro.Enumeration != null)
                        continue;

                    Field item = GenerateConstValueFromMacro(context, macro);
                    if (item == null)
                        continue;
                    wrappingClass.Fields.Add(item);
                }
            }
        }

        private static bool GetCreateWrappingClass(string className, TranslationUnit tu, out Class wrappingClass)
        {
            wrappingClass = tu.FindClass(className);
            if (wrappingClass == null)
            {
                wrappingClass = new Class {Name = className, Namespace = tu, IsStatic = true};
                return true;
            }
            return false;
        }

        public static void MoveAllIntoWrapperClass(this ILibrary library,
            string className,
            IEnumerable<TranslationUnit> ourTranslationUnits)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
                Class wrappingClass;
                GetCreateWrappingClass(className, tu, out wrappingClass);

                wrappingClass.Classes.AddRange(tu.Classes.Except(new List<Class> {wrappingClass}));
                foreach (Class decl in wrappingClass.Classes)
                {
                    decl.Namespace = wrappingClass;
                }
                tu.Classes.Clear();
                tu.Classes.Add(wrappingClass);

                wrappingClass.Functions.AddRange(tu.Functions);
                foreach (Function decl in wrappingClass.Functions)
                {
                    decl.Namespace = wrappingClass;
                }
                tu.Functions.Clear();

                wrappingClass.Enums.AddRange(tu.Enums);
                foreach (Enumeration decl in wrappingClass.Enums)
                {
                    decl.Namespace = wrappingClass;
                }
                tu.Enums.Clear();
            }
        }
    }
}
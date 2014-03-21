using System.Collections.Generic;
using System.IO;
using System.Linq;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;

namespace FFmpegBindings
{
    internal class FFMS2Library : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
            lib.GenerateClassWithConstValuesFromMacros(driver.ASTContext.TranslationUnits, "FFMS2");
            lib.CreateOverloadsForFunctionWithParamConstChar(driver.ASTContext.TranslationUnits);

            MoveAllIntoWrapperClass("FFMS2", driver.ASTContext.TranslationUnits);
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = "FFMS2";
            driver.Options.Headers.Add(@"C:\WORK\LIBS\ffms2-x86_64-shared-release-install\include\ffms.h");
            driver.Options.OutputDir = Path.Combine(@"C:\WORK\REPOS-SC\FFMS2_bindings");
            driver.Options.OutputNamespace = "FFMS2";
            driver.Options.CustomDllImport = "FFMS2_DLL_NAME";
        }

        public void SetupPasses(Driver driver)
        {
            var generatedTypesFile = new TranslationUnit("Generated.cs");
            driver.ASTContext.TranslationUnits.Add(generatedTypesFile);
            driver.TranslationUnitPasses.AddPass(new GenerateWrapperForUnsupportedArrayFieldsPass(generatedTypesFile));
            driver.TranslationUnitPasses.AddPass(new RewriteDoublePointerFunctionParametersToRef());
        }
        private static bool GetCreateWrappingClass(string className, TranslationUnit tu, out Class wrappingClass)
        {
            wrappingClass = tu.FindClass(className);
            if (wrappingClass == null)
            {
                wrappingClass = new Class { Name = className, Namespace = tu, IsStatic = true };
                return true;
            }
            return false;
        }

        public void MoveAllIntoWrapperClass(string wrappingClassName,
            IEnumerable<TranslationUnit> ourTranslationUnits)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
                Class wrappingClass;
                GetCreateWrappingClass(wrappingClassName, tu, out wrappingClass);

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
using System.IO;
using CppSharp;
using CppSharp.AST;

namespace FFmpegBindings
{
    internal class FFMS2Library : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = "FFMS2";
            driver.Options.Headers.Add(@"C:\WORK\REPOS-SC\ffms2\include\ffms.h");
            driver.Options.OutputDir = Path.Combine(@"C:\WORK\REPOS-SC\FFMS2_bindings");
            driver.Options.OutputNamespace = "FFMS2";
            driver.Options.CustomDllImport = "FFMS2_DLL_NAME";
        }

        public void SetupPasses(Driver driver)
        {
        }
    }
}
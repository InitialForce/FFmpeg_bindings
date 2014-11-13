using System.IO;
using CppSharp;
using CppSharp.AST;

namespace FFmpegBindings
{
    internal class ForesightLibrary : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = "Foresight";
            driver.Options.Headers.Add(@"Z:\Software\DEV - SDK\Foresight\SDK\2014-09-02 Foresight SDK 5.2.0.0 (64-bit beta)\ForesightAccess.h");
            driver.Options.OutputDir = Path.Combine(@"Z:\Software\DEV - SDK\Foresight\SDK");
            driver.Options.OutputNamespace = "Foresight";
            driver.Options.CustomDllImport = "ForesightAccessDLL.DLL";
            driver.Options.Defines.Add("WCHAR=char");
            driver.Options.Defines.Add("ULONG=unsigned long");
            driver.Options.Defines.Add("BOOL=int");
            driver.Options.Defines.Add("bool=int");
        }

        public void SetupPasses(Driver driver)
        {
        }
    }
}
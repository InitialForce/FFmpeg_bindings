using System.IO;
using CppSharp;
using CppSharp.AST;

namespace FFmpegBindings
{
    internal class FSALibrary : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = "FSA";
            driver.Options.Headers.Add(@"Z:\Software\DEV - SDK\BodiTrak T7\t7_sdk_1.0.006b8\lib\fsa.h   ");
            driver.Options.OutputDir = Path.Combine(@"Z:\Software\DEV - SDK\BodiTrak T7\t7_sdk_1.0.006b8\lib");
            driver.Options.OutputNamespace = "FSA";
            driver.Options.CustomDllImport = "FSA_DLL_NAME";
        }

        public void SetupPasses(Driver driver)
        {
        }
    }
}
using System.Collections.Generic;
using System.IO;
using CppSharp;
using CppSharp.AST;
using FFmpegBindings.Utilities;

namespace FFmpegBindings
{
    internal class LibraryUeye: ILibrary
    {
        public virtual void Preprocess(Driver driver, ASTContext ctx)
        {
            // it's not possible to handle va_list using p/invoke
            ctx.IgnoreFunctionsWithParameterTypeName("va_list");
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
            lib.GenerateClassWithConstValuesFromMacros(lib.TranslationUnits, "uEye");
            this.MoveAllIntoWrapperClass("uEye", lib.TranslationUnits);
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = "Foresight";
            
            driver.Options.IncludeDirs.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include");
            driver.Options.IncludeDirs.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\shared");
            driver.Options.IncludeDirs.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\um");
            driver.Options.Headers.Add(@"C:\Program Files\IDS\uEye\Develop\include\uEye.h");
            driver.Options.OutputDir = Path.Combine(@"C:\WORK\Temp\ids");
            driver.Options.OutputNamespace = "InitialForce.Video.IDS.Interop";
            driver.Options.CustomDllImport = "IDS.DLL";
            driver.Options.OutputContainerClass = "uEye";
//            driver.Options.Defines.Add("_MSC_VER");
//            driver.Options.Defines.Add("_IDS_EXPORT");
//            driver.Options.Defines.Add("IDSEXP int");
//            driver.Options.Defines.Add("IDSEXPUL unsigned long");
//            driver.Options.Defines.Add("WCHAR=char");
//            driver.Options.Defines.Add("HIDS DWORD");
//            driver.Options.Defines.Add("UINT unsigned int");
//            driver.Options.Defines.Add("ULONG unsigned long");
//            driver.Options.Defines.Add("BOOL=int");
//            driver.Options.Defines.Add("bool=int");
        }

        public void SetupPasses(Driver driver)
        {
        }
    }
}
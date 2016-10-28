using System.IO;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using FFmpegBindings.Utilities;

namespace FFmpegBindings
{
    internal class LibraryMfx : ILibrary
    {
        private const string outputContainerClass = "MFX";

        public virtual void Preprocess(Driver driver, ASTContext ctx)
        {
            // it's not possible to handle va_list using p/invoke
            ctx.IgnoreFunctionsWithParameterTypeName("va_list");
            ctx.MergeStructAndPtrStructName();
            RenameEnums(ctx);
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
            lib.GenerateClassWithConstValuesFromMacros(lib.TranslationUnits, outputContainerClass);
            this.MoveAllIntoWrapperClass(outputContainerClass, lib.TranslationUnits);
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = outputContainerClass;

            driver.Options.Headers.Add(
                @"C:\WORK\REPOS-SC\MINGW-packages\mingw-w64-mfx_dispatch\mingw-w64-x86_64-mfx_dispatch-1.0-1-any.pkg\mingw64\include\mfx\mfxvideo.h");
            driver.Options.Headers.Add(
                @"C:\WORK\REPOS-SC\MINGW-packages\mingw-w64-mfx_dispatch\mingw-w64-x86_64-mfx_dispatch-1.0-1-any.pkg\mingw64\include\mfx\mfxenc.h.h");
            driver.Options.OutputDir = Path.Combine(@"C:\WORK\Temp\Mfx");
            driver.Options.OutputNamespace = "InitialForce.Video.IntelMediaSdk.Interop";
            driver.Options.CustomDllImport = "\"libmfx-0.dll\"";
            driver.Options.OutputContainerClass = outputContainerClass;
        }

        public void SetupPasses(Driver driver)
        {
            driver.TranslationUnitPasses.RenameWithPattern("MFX_IMPL", "IMPL", RenameTargets.Enum);
            driver.TranslationUnitPasses.RenameWithPattern("MFX_FOURCC", "FOURCC", RenameTargets.Enum);
            driver.TranslationUnitPasses.RemovePrefix("MFX_HANDLE_", RenameTargets.EnumItem);
            driver.TranslationUnitPasses.RemovePrefix("MFX_SKIPMODE_", RenameTargets.EnumItem);
            driver.TranslationUnitPasses.RemovePrefix("MFX_IMPL_", RenameTargets.EnumItem);
            driver.TranslationUnitPasses.RemovePrefix("MFX_FOURCC_", RenameTargets.EnumItem);
            driver.TranslationUnitPasses.RemovePrefix("MFX_PRIORITY_", RenameTargets.EnumItem);
            driver.TranslationUnitPasses.RemovePrefix("MFX_");
            driver.TranslationUnitPasses.RemovePrefix("MFX", RenameTargets.Method|RenameTargets.Function);
        }


        public void RenameEnums(ASTContext context)
        {
            foreach (var unit in context.TranslationUnits)
            {
                foreach (var enumeration in unit.Enums)
                {
                    if (enumeration.Name.StartsWith("E_"))
                    {
                        enumeration.Name = enumeration.Name.Substring(2);
                    }
                }
            }
        }
    }
}
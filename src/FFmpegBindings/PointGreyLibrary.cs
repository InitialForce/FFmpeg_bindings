using System;
using System.Collections.Generic;
using System.IO;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;

namespace FFmpegBindings
{
    internal class PointGreyLibraryGUI : PointGreyLibraryBase
    {
        public PointGreyLibraryGUI(PointGreyLibrary pointGreyLibrary)
        {
            _dependentLibraries.Add(pointGreyLibrary);
        }

        public override void Setup(Driver driver)
        {
            driver.Options.LibraryName = "FlyCapture2GUI";
            driver.Options.Headers.Add(@"C:\Program Files\Point Grey Research\FlyCapture2\include\C\FlyCapture2GUI_C.h");
            driver.Options.OutputDir =
                Path.Combine(
                    @"C:\WORK\REPOS-SC\ScDesktop\code\build\swingcatalyst\Video\SwingCatalyst.Video.PointGrey\Interop");
            driver.Options.OutputNamespace = "SwingCatalyst.Video.PointGrey.Interop";
            driver.Options.OutputContainerClass = "FlyCapture2GUI_C";
            driver.Options.CustomDllImport = "\"FlyCapture2GUI_C.dll\"";
        }
    }

    internal class PointGreyLibrary : PointGreyLibraryBase
    {
        public override void Setup(Driver driver)
        {
            driver.Options.LibraryName = "FlyCapture2";
            driver.Options.Headers.Add(@"C:\Program Files\Point Grey Research\FlyCapture2\include\C\FlyCapture2_C.h");
            driver.Options.Headers.Add(
                @"C:\Program Files\Point Grey Research\FlyCapture2\include\C\FlyCapture2Defs_C.h");
            driver.Options.OutputDir =
                Path.Combine(
                    @"C:\WORK\REPOS-SC\ScDesktop\code\build\swingcatalyst\Video\SwingCatalyst.Video.PointGrey\Interop");
            driver.Options.OutputNamespace = "SwingCatalyst.Video.PointGrey.Interop";
            driver.Options.OutputContainerClass = "FlyCapture2_C";
            driver.Options.CustomDllImport = "\"FlyCapture2_C.dll\"";
        }
    }

    internal abstract class PointGreyLibraryBase : IComplexLibrary
    {
        protected readonly List<IComplexLibrary> _dependentLibraries = new List<IComplexLibrary>();

        public void Preprocess(Driver driver, ASTContext ctx)
        {
            throw new NotImplementedException();
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
            throw new NotImplementedException();
        }

        public abstract void Setup(Driver driver);

        public void SetupPasses(Driver driver)
        {
            driver.TranslationUnitPasses.AddPass(new RewriteDoublePointerFunctionParametersToRef());
            driver.TranslationUnitPasses.AddPass(new RegexRenamePass(@"^_(\w+)", @"$1",
                RenameTargets.Class | RenameTargets.Enum));

            driver.TranslationUnitPasses.AddPass(new TypeParameterRenamePass("context",
                new BuiltinType(PrimitiveType.IntPtr), RenameTargets.Parameter | RenameTargets.Function));

            driver.TranslationUnitPasses.AddPass(new TypeParameterRenamePass("pContext",
                new BuiltinType(PrimitiveType.IntPtr), RenameTargets.Parameter | RenameTargets.Function));
        }

        public string LibraryName { get; private set; }

        public IEnumerable<IComplexLibrary> DependentLibraries
        {
            get { return _dependentLibraries; }
        }

        public TranslationUnit GeneratedTypesFile { get; private set; }

        public void Postprocess(Driver driver, ASTContext astContext, IEnumerable<ASTContext> @select)
        {
            astContext.GenerateClassWithConstValuesFromMacros(driver.ASTContext.TranslationUnits, "PointGrey");
            astContext.CreateOverloadsForFunctionWithParamConstChar(driver.ASTContext.TranslationUnits);
        }

        public void Preprocess(Driver driver, ASTContext astContext, IEnumerable<ASTContext> @select)
        {
        }
    }
}
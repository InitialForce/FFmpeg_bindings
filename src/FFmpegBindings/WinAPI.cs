using System.Collections.Generic;
using System.Linq;
using CppSharp;
using CppSharp.AST;
using CppSharp.Passes;
using FFmpegBindings.Utilities;

namespace FFmpegBindings
{
    public class WinAPI : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {
            ctx.IgnoreFunctionsWithParameterTypeName("va_list");
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
        }

        public void Setup(Driver driver)
        {
            driver.Options.LibraryName = "winapi";
            driver.Options.IncludeDirs.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include");
            driver.Options.IncludeDirs.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\shared");
            driver.Options.IncludeDirs.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\um");
            driver.Options.OutputDir = @"C:\WORK\REPOS-SC\CppSharp\examples\SDL\wingdi";
            driver.Options.OutputNamespace = "winapi";
            driver.Options.OutputContainerClass = "winapi";
            driver.Options.Headers.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\um\windows.h");
            driver.Options.Headers.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\shared\winapifamily.h");
            driver.Options.Headers.Add(@"C:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h");

            driver.TranslationUnitPasses.AddPass(new CheckWinMacroPass());
        }

        public void SetupPasses(Driver driver)
        {
        }

        /// <summary>
        ///     Translate applicable Microsoft source-code annotation language (SAL) annotations to c# attributes
        ///     http://msdn.microsoft.com/en-us/library/hh916383.aspx
        /// </summary>
        public class CheckWinMacroPass : TranslationUnitPass
        {
            public override bool VisitParameterDecl(Parameter parameter)
            {
                if (!VisitDeclaration(parameter))
                    return false;

                IEnumerable<MacroExpansion> expansions = parameter.PreprocessedEntities.OfType<MacroExpansion>();

                if (expansions.Any(e => e.Text == "_In_"))
                    parameter.Usage = ParameterUsage.In;

                if (expansions.Any(e => e.Text == "_Out_"))
                    parameter.Usage = ParameterUsage.Out;

                if (expansions.Any(e => e.Text == "_Inout_"))
                    parameter.Usage = ParameterUsage.InOut;

                // TODO: add support for more function parameter annotations (optional parameter etc)
                // http://msdn.microsoft.com/en-us/library/hh916382.aspx

                return true;
            }

            public override bool VisitFieldDecl(Field field)
            {
                if (!VisitDeclaration(field))
                    return false;

                // TODO: add support for field annotations
                // http://msdn.microsoft.com/en-us/library/jj159528.aspx

                return true;
            }
        }
    }
}
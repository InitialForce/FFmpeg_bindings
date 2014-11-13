using System.Collections.Generic;
using System.IO;
using System.Linq;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;
using FFmpegBindings.Utilities;

namespace FFmpegBindings
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
//            RunSingle(new WinAPI());
//            RunSingle(new FSALibrary());
//            RunSingle(new ForesightLibrary());

            var pointGreyLibrary = new PointGreyLibrary();
            var pointGreyLibraryGui = new PointGreyLibraryGUI(pointGreyLibrary);
            GenerateComplexLibraries(new IComplexLibrary[]
            {
                pointGreyLibrary,
                pointGreyLibraryGui
            });
//            RunSingle(new FFMS2Library());
            //            Environment.Exit(0);

//            GenerateFFmpeg();
        }

         public static void RunSingle(ILibrary library)
        {
            var options = new DriverOptions
            {
                TargetTriple = "i686-pc-win32",
                //            TargetTriple = "x86_64-pc-win64",
                Gnu99Mode = true,
                Verbose = false,
            };

            var Log = new TextDiagnosticPrinter();
            var driver = new Driver(options, Log);

            library.Setup(driver);
            driver.Setup();

            Log.Verbose = driver.Options.Verbose;

            if (!options.Quiet)
                Log.EmitMessage("Parsing libraries...");

            if (!driver.ParseLibraries())
                return;

            if (!options.Quiet)
                Log.EmitMessage("Indexing library symbols...");

            driver.Symbols.IndexSymbols();

            if (!options.Quiet)
                Log.EmitMessage("Parsing code...");

            if (!driver.ParseCode())
                return;

            if (!options.Quiet)
                Log.EmitMessage("Processing code...");

            library.Preprocess(driver, driver.ASTContext);

            driver.SetupPasses(library);

            driver.ProcessCode();
            library.Postprocess(driver, driver.ASTContext);

            if (!options.Quiet)
                Log.EmitMessage("Generating code...");

            var outputs = driver.GenerateCode();

            foreach (var output in outputs)
            {
                foreach (var pass in driver.GeneratorOutputPasses.Passes)
                {
                    pass.Driver = driver;
                    pass.VisitGeneratorOutput(output);
                }
            }

            driver.WriteCode(outputs);
            if (driver.Options.IsCSharpGenerator)
                driver.CompileCode();
        }

        private static void GenerateFFmpeg()
        {
//            string versionString = "2.1.3";
            string versionString = "2.4";
            //            string versionString = "1.0.7";
            var ffmpegInstallDir = new DirectoryInfo(@"..\..\..\..\..\ffmpeg\" + versionString);
            var outputDir = new DirectoryInfo(@"..\..\..\..\" + versionString);

            var utilityClass = new TranslationUnit
            {
                Name = "generated",
                FilePath = Path.Combine(ffmpegInstallDir.FullName, "generated.h"),
            };

            var avutilLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avutil", "avutil-if-54.dll", outputDir);
            var avcodecLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avcodec", "avcodec-if-56.dll",
                outputDir, new List<string>
                {
                    "old_codec_ids.h",
                    "dxva2.h",
                    "vda.h",
                    "vdpau.h",
                    "xvmc.h"
                }, new[] {avutilLib});

            var avformatLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avformat", "avformat-if-56.dll",
                outputDir, null,
                new[] {avutilLib, avcodecLib});

            var swresampleLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "swresample", "swresample-if-1.dll",
                outputDir,
                null, new[] {avutilLib});

            var swscaleLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "swscale", "swscale-if-3.dll",
                outputDir, null, new[] {avutilLib});

            var avfilterLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avfilter", "avfilter-if-5.dll",
                outputDir, null, new[] {avutilLib, swresampleLib, swscaleLib, avcodecLib, avformatLib});

            var avdeviceLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avdevice", "avdevice-if-56.dll",
                outputDir);

            GenerateComplexLibraries(new[]
            {
                avcodecLib,
                avformatLib,
                swresampleLib,
                swscaleLib,
                avfilterLib,
                avdeviceLib,
                avutilLib
            });
        }

        private static void GenerateComplexLibraries(IList<IComplexLibrary> complexLibraries)
        {
            var log = new TextDiagnosticPrinter();

            // sort topoligically (by dependencies)
            IEnumerable<IComplexLibrary> sorted = complexLibraries.TSort(l => l.DependentLibraries);
            var drivers = new List<S>();

            log.EmitMessage("Parsing libraries...");
            foreach (IComplexLibrary lib in sorted)
            {
                List<Driver> dependents = drivers.Select(s => s.Driver).ToList();
                drivers.Add(new S
                {
                    Library = lib,
                    Driver = GenerateLibrary(log, lib, dependents),
                    Dependents = dependents,
                });
            }

            log.EmitMessage("Postprocess ...");
            var generated = new List<TranslationUnit>();
            foreach (S s in drivers)
            {
                var library = s.Library;
                Driver driver = s.Driver;

                library.Postprocess(driver, driver.ASTContext, s.Dependents.Select(d => d.ASTContext));
            }

            log.EmitMessage("Generating ...");
            foreach (S s in drivers)
            {
                Driver driver = s.Driver;

                List<GeneratorOutput> outputs = driver.GenerateCode(generated);

                foreach (GeneratorOutput output in outputs)
                {
                    foreach (GeneratorOutputPass pass in driver.GeneratorOutputPasses.Passes)
                    {
                        pass.Driver = driver;
                        pass.VisitGeneratorOutput(output);
                    }
                }

                driver.WriteCode(outputs);

                generated.AddRange(outputs.Select(t => t.TranslationUnit));
            }
        }

        private static Driver GenerateLibrary(TextDiagnosticPrinter log, IComplexLibrary library,
            IList<Driver> dependentLibraries)
        {
            var options = new DriverOptions
            {
                TargetTriple = "i686-pc-win32",
                //            TargetTriple = "x86_64-pc-win64",
                Gnu99Mode = true,
                Verbose = false,
                ForceNativeTypePrinting = true,
            };

            var driver = new Driver(options, log);
            log.Verbose = driver.Options.Verbose;

            library.Setup(driver);
            driver.Setup();

            foreach (Driver dependentLibDriver in dependentLibraries)
            {
                foreach (TranslationUnit tu in dependentLibDriver.ASTContext.TranslationUnits)
                {
                    driver.ASTContext.TranslationUnits.Add(tu);
                }
            }
            if(library.LibraryName == "avutil")
                driver.ASTContext.TranslationUnits.Add(library.GeneratedTypesFile);

            log.EmitMessage("Parsing libraries...");

            if (!driver.ParseLibraries())
                return driver;

            if (!options.Quiet)
                log.EmitMessage("Indexing library symbols...");

            driver.Symbols.IndexSymbols();

            if (!options.Quiet)
                log.EmitMessage("Parsing code...");

            if (!driver.ParseCode())
                return driver;

            if (!options.Quiet)
                log.EmitMessage("Processing code...");

            //            driver.ASTContext.ResolveUnifyIncompleteClassDeclarationsFromSubLibs(dependentLibraries);

            library.Preprocess(driver, driver.ASTContext, dependentLibraries.Select(d => d.ASTContext));

            driver.SetupPasses(library);

            driver.ProcessCode();

            return driver;
        }

        private struct S
        {
            public Driver Driver { get; set; }
            public List<Driver> Dependents { get; set; }
            public IComplexLibrary Library { get; set; }
        }
    }

    public interface IComplexLibrary : ILibrary
    {
        string LibraryName { get; }
        IEnumerable<IComplexLibrary> DependentLibraries { get; }
        TranslationUnit GeneratedTypesFile { get; }
        void Postprocess(Driver driver, ASTContext astContext, IEnumerable<ASTContext> @select);
        void Preprocess(Driver driver, ASTContext astContext, IEnumerable<ASTContext> @select);
    }
}
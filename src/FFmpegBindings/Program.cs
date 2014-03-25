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
//            GenerateLibrary(new WinAPI());
//            ConsoleDriver.Run(new FSALibrary());
//            ConsoleDriver.Run(new FFMS2Library());
            //            Environment.Exit(0);

            GenerateFFmpeg();
        }

        private static void GenerateFFmpeg()
        {
//            string versionString = "2.1.3";
            string versionString = "2.2";
            //            string versionString = "1.0.7";
            var ffmpegInstallDir = new DirectoryInfo(@"..\..\..\..\..\ffmpeg\" + versionString);
            var outputDir = new DirectoryInfo(@"..\..\..\..\" + versionString);

            var utilityClass = new TranslationUnit
            {
                Name = "generated",
                FilePath = Path.Combine(ffmpegInstallDir.FullName, "generated.h"),
            };

            var avutilLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avutil", "avutil-if-52.dll", outputDir);
            var avcodecLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avcodec", "avcodec-if-55.dll",
                outputDir, new List<string>
                {
                    "old_codec_ids.h",
                    "dxva2.h",
                    "vda.h",
                    "vdpau.h",
                    "xvmc.h"
                }, new[] {avutilLib});

            var avformatLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avformat", "avformat-if-55.dll",
                outputDir, null,
                new[] {avutilLib, avcodecLib});

            var swresampleLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "swresample", "swresample-if-0.dll",
                outputDir,
                null, new[] {avutilLib});

            var swscaleLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "swscale", "swscale-if-2.dll",
                outputDir, null, new[] {avutilLib});

            var avfilterLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avfilter", "avfilter-if-3.dll",
                outputDir, null, new[] {avutilLib, swresampleLib, swscaleLib, avcodecLib, avformatLib});

            var avdeviceLib = new FFmpegSubLibrary(ffmpegInstallDir, utilityClass, "avdevice", "avdevice-if-55.dll",
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

        private static void GenerateComplexLibraries(IList<FFmpegSubLibrary> complexLibraries)
        {
            var log = new TextDiagnosticPrinter();

            // sort topoligically (by dependencies)
            IEnumerable<FFmpegSubLibrary> sorted = complexLibraries.TSort(l => l.DependentLibraries);
            var drivers = new List<S>();

            log.EmitMessage("Parsing libraries...");
            foreach (FFmpegSubLibrary lib in sorted)
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
                FFmpegSubLibrary library = s.Library;
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

        private static Driver GenerateLibrary(TextDiagnosticPrinter log, FFmpegSubLibrary library,
            IList<Driver> dependentLibraries)
        {
            var options = new DriverOptions
            {
                TargetTriple = "i686-pc-win32",
                //            TargetTriple = "x86_64-pc-win64",
                Gnu99Mode = true,
                Verbose = false,
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
            public FFmpegSubLibrary Library { get; set; }
        }
    }
}
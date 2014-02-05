using System.Collections.Generic;
using System.IO;
using CppSharp;
using CppSharp.Generators;
using CppSharp.Passes;

namespace FFmpegBindings
{
    internal static class Program
    {

        public static void Main(string[] args)
        {
            var versionString = "1.0.7";
            var ffmpegInstallDir = new DirectoryInfo(@"..\..\..\..\..\ffmpeg\" + versionString);
            var outputDir = new DirectoryInfo(@"..\..\..\..\..\src\" + versionString);

            const string libraryPostfix = "if";
            var avcodecLib = new FFmpegSubLibrary(ffmpegInstallDir, "avcodec", libraryPostfix, outputDir, new List<string>
            {
                "old_codec_ids.h",
                "dxva2.h",
                "vda.h",
                "vdpau.h",
                "xvmc.h"
            });
            var avutilLib = new FFmpegSubLibrary(ffmpegInstallDir, "avutil", libraryPostfix, outputDir);
            var avformatLib = new FFmpegSubLibrary(ffmpegInstallDir, "avformat", libraryPostfix, outputDir);
            var swresampleLib = new FFmpegSubLibrary(ffmpegInstallDir, "swresample", libraryPostfix, outputDir);
            var swscaleLib = new FFmpegSubLibrary(ffmpegInstallDir, "swscale", libraryPostfix, outputDir);
            var avfilterLib = new FFmpegSubLibrary(ffmpegInstallDir, "avfilter", libraryPostfix, outputDir);
            var avdeviceLib = new FFmpegSubLibrary(ffmpegInstallDir, "avdevice", libraryPostfix, outputDir);

            Generate(avutilLib);
            Generate(avcodecLib);
            Generate(avformatLib);
            Generate(swresampleLib);
            Generate(swscaleLib);
            Generate(avfilterLib);
            Generate(avdeviceLib);

        }

        /// <summary>
        ///     Some of the code from ConsoleDriver.Run
        /// </summary>
        /// <param name="library"></param>
        private static void Generate(ILibrary library)
        {
            var options = new DriverOptions
            {
                TargetTriple = "i686-pc-win32",
                //            TargetTriple = "x86_64-pc-win64",
                Gnu99Mode = true,
                Verbose = true,
            };

            var log = new TextDiagnosticPrinter();
            var driver = new Driver(options, log);
            log.Verbose = driver.Options.Verbose;

            library.Setup(driver);
            driver.Setup();

            if (!options.Quiet)
                log.EmitMessage("Parsing libraries...");

            if (!driver.ParseLibraries())
                return;

            if (!options.Quiet)
                log.EmitMessage("Indexing library symbols...");

            driver.Symbols.IndexSymbols();

            if (!options.Quiet)
                log.EmitMessage("Parsing code...");

            if (!driver.ParseCode())
                return;

            if (!options.Quiet)
                log.EmitMessage("Processing code...");

            library.Preprocess(driver, driver.ASTContext);

            driver.SetupPasses(library);

            driver.ProcessCode();
            library.Postprocess(driver, driver.ASTContext);

            if (!options.Quiet)
                log.EmitMessage("Generating code...");

            List<GeneratorOutput> outputs = driver.GenerateCode();

            foreach (GeneratorOutput output in outputs)
            {
                foreach (GeneratorOutputPass pass in driver.GeneratorOutputPasses.Passes)
                {
                    pass.Driver = driver;
                    pass.VisitGeneratorOutput(output);
                }
            }

            driver.WriteCode(outputs);
            if (driver.Options.IsCSharpGenerator)
                driver.CompileCode();

            //            ConsoleDriver.Run(library);
        }
    }
}
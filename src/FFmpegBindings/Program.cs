using CppSharp;

namespace FFmpegBindings
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Generate(new AVUtilLibrary());
            //            Generate(new SDL());
        }

        /// <summary>
        /// Some of the code from ConsoleDriver.Run
        /// </summary>
        /// <param name="library"></param>
        private static void Generate(ILibrary library)
        {
            var options = new DriverOptions();

//            options.TargetTriple = "x86_64-pc-win64";
            options.TargetTriple = "i686-pc-win32";

            var Log = new TextDiagnosticPrinter();
            var driver = new Driver(options, Log);

            library.Setup(driver);
            driver.Setup();

//            Log.Verbose = driver.Options.Verbose;

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

            //            ConsoleDriver.Run(library);
        }
    }
}
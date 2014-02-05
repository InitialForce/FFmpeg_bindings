using System.Collections.Generic;
using System.IO;
using System.Linq;
using CppSharp;
using CppSharp.AST;

namespace FFmpegBindings
{
    internal class FFmpegSubLibrary : ILibrary
    {
        private readonly IEnumerable<string> _filesToIgnore;
        private readonly string _libraryName;
        private readonly string _libraryPostfix;
        private readonly string _dllName;
        private readonly DirectoryInfo _outputDir;
        private readonly DirectoryInfo _includeDir;
        private int _majorVersion;

        public FFmpegSubLibrary(DirectoryInfo includeDir, string libraryName, string libraryPostfix, DirectoryInfo outputDir, IEnumerable<string> filesToIgnore = null)
        {
            _includeDir = includeDir;
            if (!_includeDir.Exists)
                throw new DirectoryNotFoundException(_includeDir.FullName);

            _libraryName = libraryName;
            _libraryPostfix = libraryPostfix;

            _outputDir = outputDir;
            _filesToIgnore = filesToIgnore ?? Enumerable.Empty<string>();

            _majorVersion = ParseMajorVersion();

            _dllName = string.Format("{0}-{1}-{2}.dll", _libraryName, libraryPostfix, _majorVersion);
        }

        private int ParseMajorVersion()
        {
            var versionFilePath = Path.Combine(_includeDir.FullName, "lib" + _libraryName, "version.h");
            string[] headerTextLines;
            if (File.Exists(versionFilePath))
            {
                headerTextLines = File.ReadAllLines(versionFilePath);
            }
            else
            {
                var libraryHeader = Path.Combine(_includeDir.FullName, "lib" + _libraryName, _libraryName +".h");
                headerTextLines = File.ReadAllLines(libraryHeader);
            }
            var majorDefineTextTag = "LIB" + _libraryName.ToUpper() + "_VERSION_MAJOR";
            var majorDefineLineText = headerTextLines.First(a => a.Contains("#define") && a.Contains(majorDefineTextTag));

            return int.Parse(majorDefineLineText.Split(' ').Last());
        }

        public virtual void Preprocess(Driver driver, ASTContext ctx)
        {
            // ignore the other ffmpeg sublibraries (e.g. don't include avutil stuff when generating for avcodec)
            foreach (DirectoryInfo subLibDir in _includeDir.GetDirectories())
            {
                if (subLibDir.Name.Contains(_libraryName))
                    continue;

                foreach (FileInfo headerFile in subLibDir.GetFiles())
                {
                    foreach (
                        TranslationUnit unit in ctx.TranslationUnits.FindAll(m => m.FilePath == headerFile.FullName))
                    {
                        unit.IsGenerated = false;
                        unit.IsProcessed = true;
                        unit.ExplicityIgnored = false;
                    }
                }
            }
        }

        public virtual void Postprocess(Driver driver, ASTContext lib)
        {
        }

        public virtual void Setup(Driver driver)
        {
            driver.Options.LibraryName = _dllName;
            string libName = "lib" + _libraryName;
            driver.Options.IncludeDirs.Add(_includeDir.FullName);
            driver.Options.OutputDir = Path.Combine(_outputDir.FullName, libName);
            driver.Options.OutputNamespace = libName;
            string combine = Path.Combine(_includeDir.FullName, libName);
            foreach (FileInfo headerFile in Directory.GetFiles(combine).Select(a => new FileInfo(a)))
            {
                string item = Path.Combine(libName, headerFile.Name);
                if (ShouldIncludeHeader(headerFile))
                {
                    driver.Options.Headers.Add(item);
                }
            }
        }

        public virtual void SetupPasses(Driver driver)
        {
        }

        protected virtual bool ShouldIncludeHeader(FileInfo headerFileName)
        {
            if (_filesToIgnore.Contains(headerFileName.Name))
                return false;
            return true;
        }
    }
}
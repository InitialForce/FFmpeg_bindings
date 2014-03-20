using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using CppSharp;
using CppSharp.AST;
using CppSharp.AST.Extensions;
using CppSharp.Passes;
using FFmpegBindings.Passes;
using FFmpegBindings.Utilities;

namespace FFmpegBindings
{
    [DebuggerDisplay("{LibraryName}")]
    public class FFmpegSubLibrary : ILibrary
    {
        private readonly IEnumerable<string> _filesToIgnore;
        private readonly DirectoryInfo _includeDir;
        private readonly DirectoryInfo _outputDir;

        public FFmpegSubLibrary(DirectoryInfo includeDir, TranslationUnit generatedTypesFile, string libraryName,
            string dllName, DirectoryInfo outputDir, IEnumerable<string> filesToIgnore = null,
            IEnumerable<FFmpegSubLibrary> dependentLibraries = null)
        {
            _includeDir = includeDir;
            GeneratedTypesFile = generatedTypesFile;
            if (!_includeDir.Exists)
                throw new DirectoryNotFoundException(_includeDir.FullName);

            LibraryName = libraryName;
            LibraryNameSpace = "lib" + LibraryName;
            DllName = dllName;

            _outputDir = outputDir;
            DependentLibraries = dependentLibraries ?? Enumerable.Empty<FFmpegSubLibrary>();
            _filesToIgnore = filesToIgnore ?? Enumerable.Empty<string>();
            OutputNamespace = "FFmpeg";
        }

        public TranslationUnit GeneratedTypesFile { get; private set; }

        public string LibraryName { get; private set; }
        public string DllName { get; private set; }
        public string LibraryNameSpace { get; private set; }
        public IEnumerable<FFmpegSubLibrary> DependentLibraries { get; private set; }
        public string OutputNamespace { get; private set; }

        public void Preprocess(Driver driver, ASTContext ctx)
        {
            throw new NotImplementedException();
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
            throw new NotImplementedException();
        }

        public virtual void Setup(Driver driver)
        {
            driver.Options.LibraryName = DllName;
            driver.Options.IncludeDirs.Add(_includeDir.FullName);
            driver.Options.OutputDir = Path.Combine(_outputDir.FullName, LibraryNameSpace);
            driver.Options.OutputNamespace = "FFmpeg";
            driver.Options.CustomDllImport = LibraryName.ToUpper(CultureInfo.InvariantCulture) + "_DLL_NAME";
            //            driver.Options.OutputClass = LibraryNameSpace;
            string combine = Path.Combine(_includeDir.FullName, LibraryNameSpace);
            foreach (FileInfo headerFile in Directory.GetFiles(combine).Select(a => new FileInfo(a)))
            {
                string item = Path.Combine(LibraryNameSpace, headerFile.Name);
                if (ShouldIncludeHeader(headerFile))
                {
                    driver.Options.Headers.Add(item);
                }
            }
            foreach (FFmpegSubLibrary dependentLibrary in DependentLibraries)
            {
                string outputNamespace = dependentLibrary.OutputNamespace;
                if (!driver.Options.DependentNameSpaces.Contains(outputNamespace))
                {
                    driver.Options.DependentNameSpaces.Add(outputNamespace);
                }
            }
        }

        public virtual void SetupPasses(Driver driver)
        {
            //                TranslationUnitPasses.AddPass(new UnwrapUnsupportedArraysPass());
            driver.TranslationUnitPasses.AddPass(new GenerateWrapperForUnsupportedArrayFieldsPass(GeneratedTypesFile));
            driver.TranslationUnitPasses.AddPass(new RewriteDoublePointerFunctionParametersToRef());
            driver.TranslationUnitPasses.AddPass(new FFmpegMacrosPass());
        }

        public virtual void Preprocess(Driver driver, ASTContext ctx, IEnumerable<ASTContext> dependentContexts)
        {
            // ignore the other ffmpeg sublibraries (e.g. don't include avutil stuff when generating for avcodec)
            //            foreach (var dependentLibDriver in _dependentLibraries)
            //            {
            //                foreach (var translationUnit in dependentLibDriver.ASTContext.TranslationUnits)
            //                {
            //                    lib.TranslationUnits.Add(translationUnit);
            //                }
            //            }
            //            foreach (DirectoryInfo subLibDir in _includeDir.GetDirectories())
            //            {
            //                if (subLibDir.Name.Contains(LibraryName))
            //                    continue;
            //
            //                foreach (FileInfo headerFile in subLibDir.GetFiles())
            //                {
            //                    foreach (
            //                        TranslationUnit unit in lib.TranslationUnits.FindAll(m => m.FilePath == headerFile.FullName))
            //                    {
            //                        unit.IsGenerated = false;
            //                        unit.ExplicityIgnored = true;
            //                    }
            //                }
            //            }

            //          ILP32 	LP64 	LLP64 	ILP64
            //char 	    8 	    8 	    8 	    8
            //short 	16 	    16 	    16 	    16
            //int 	    32 	    32 	    32 	    64
            //long 	    32 	    64 	    32 	    64
            //long long 64 	    64 	    64 	    64
            //size_t 	32 	    64 	    64 	    64
            //pointer 	32 	    64 	    64 	    64

            //ptrdiff_t 	32 	64 	
            //size_t 	    32 	64 	
            //intptr_tuintptr_t, SIZE_T, SSIZE_T, INT_PTR, DWORD_PTR, etc 32 	64 
            //time_t 	    32 	64


            ctx.IgnoreFunctionWithName("av_hex_dump");
            ctx.IgnoreFunctionWithName("av_pkt_dump2");
            ctx.IgnoreFunctionWithName("av_bprint_strftime");
            ctx.IgnoreFunctionWithName("av_dbl2ext");
            ctx.IgnoreFunctionWithName("av_small_strptime");
            ctx.IgnoreFunctionWithName("av_timegm");


            ctx.ConvertTypesToPortable(t => t.Declaration.Name == "size_t", PrimitiveType.UIntPtr);
            ctx.ConvertTypesToPortable(t => t.Declaration.Name == "time_t", PrimitiveType.UIntPtr);
            ctx.ConvertTypesToPortable(t => t.Declaration.Name == "ptrdiff_t", PrimitiveType.UIntPtr);

            ctx.ChooseAndPromoteIncompleteClass();
            //            lib.ResolveUnifyIncompleteClassDeclarationsFromSubLibs(DependentLibraries);
            //            lib.ResolveUnifyIncompleteClassDeclarations();
            // it's not possible to handle va_list using p/invoke
            ctx.IgnoreFunctionsWithParameterTypeName("va_list");
        }

        public virtual void Postprocess(Driver driver, ASTContext lib, IEnumerable<ASTContext> dependentContexts)
        {
            List<TranslationUnit> ourTranslationUnits = GetLibOnlyTranslationUnits(lib, dependentContexts);

            lib.GenerateClassWithConstValuesFromMacros(ourTranslationUnits, LibraryNameSpace);
            lib.CreateOverloadsForFunctionWithParamConstChar(ourTranslationUnits);
            this.MoveAllIntoWrapperClass(ourTranslationUnits);
        }

        private static List<TranslationUnit> GetLibOnlyTranslationUnits(ASTContext lib,
            IEnumerable<ASTContext> dependentContexts)
        {
            List<TranslationUnit> ourTranslationUnits = lib.TranslationUnits.Where(
                o => !dependentContexts.Any(d => d.TranslationUnits.Any(t => t == o.TranslationUnit))).ToList();
            return ourTranslationUnits;
        }

        protected virtual bool ShouldIncludeHeader(FileInfo headerFileName)
        {
            if (_filesToIgnore.Contains(headerFileName.Name))
                return false;
            return true;
        }
    }

    public abstract class CreateFunctionOverload : TranslationUnitPass
    {
        public override bool VisitFunctionDecl(Function function)
        {
            if (CheckModify(function))
            {
                var clone = new Function(function);
                ModifyFunction(clone);
                int index = function.Namespace.Functions.IndexOf(function);
                function.Namespace.Functions.Insert(index + 1, clone);
            }
            return true;
        }

        protected abstract void ModifyFunction(Function function);

        protected abstract bool CheckModify(Function arg);
    }

    public class RewriteDoublePointerFunctionParametersToRef : CreateFunctionOverload
    {
        private bool IsDoublePointer(Parameter parameter)
        {
            var type = parameter.Type as PointerType;
            if (type != null)
            {
                return type.Pointee.Desugar() is PointerType;
            }
            return false;
        }

        protected override void ModifyFunction(Function function)
        {
            function.Parameters = function.Parameters.Select(ModifyParameter).ToList();
        }

        private Parameter ModifyParameter(Parameter parameter)
        {
            if (IsDoublePointer(parameter))
            {
                return new Parameter(parameter)
                {
                    Usage = ParameterUsage.InOut,
                    QualifiedType = new QualifiedType
                    {
                        Type = ((PointerType) parameter.Type).Pointee,
                        Qualifiers = parameter.QualifiedType.Qualifiers
                    }
                };
            }
            return parameter;
        }

        protected override bool CheckModify(Function arg)
        {
            return arg.Parameters.Any(IsDoublePointer);
        }
    }

    public static class Extensions
    {
        public static Field GenerateConstValueFromMacro(this ASTContext context,
            MacroExpansion macro)
        {
            if (macro.Text != null && macro.Text.Contains("M_PI"))
                Debugger.Break();

            PrimitiveTypeExpression builtinTypeExpression = PrimitiveTypeExpression.TryCreate(macro.Text);
            if (builtinTypeExpression == null)
                return null;
            var valueType = new QualifiedType(new BuiltinType(builtinTypeExpression.Type))
            {
                Qualifiers = new TypeQualifiers {IsConst = true}
            };
            var item = new Field
            {
                Name = macro.Name,
                DebugText = macro.DebugText,
                Access = AccessSpecifier.Public,
                Expression =
                    builtinTypeExpression,
                QualifiedType = valueType
            };

            return item;
        }

        public static Field GenerateConstValueFromMacro(this ASTContext context,
            MacroDefinition macro)
        {
            if (macro.Expression != null && macro.Expression.Contains("M_PI"))
                Debugger.Break();

            PrimitiveTypeExpression builtinTypeExpression = PrimitiveTypeExpression.TryCreate(macro.Expression);
            if (builtinTypeExpression == null)
                return null;
            var valueType = new QualifiedType(new BuiltinType(builtinTypeExpression.Type))
            {
                Qualifiers = new TypeQualifiers {IsConst = true}
            };
            var item = new Field
            {
                Name = macro.Name,
                DebugText = macro.DebugText,
                Access = AccessSpecifier.Public,
                Expression =
                    builtinTypeExpression,
                QualifiedType = valueType
            };

            return item;
        }

        public static void CreateOverloadsForFunctionWithParamConstChar(this ASTContext context,
            IEnumerable<TranslationUnit> ourTranslationUnits)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
            }
        }

        public static void GenerateClassWithConstValuesFromMacros(this ASTContext context,
            IEnumerable<TranslationUnit> ourTranslationUnits, string className)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
                Class wrappingClass;
                if (GetCreateWrappingClass(className, tu, out wrappingClass))
                {
                    tu.Classes.Add(wrappingClass);
                }
                foreach (
                    MacroExpansion macro in
                        tu.PreprocessedEntities.OfType<MacroExpansion>())
                {
                    Field item = GenerateConstValueFromMacro(context, macro);
                    if (item == null)
                        continue;
                    wrappingClass.Fields.Add(item);
                }
                foreach (
                    MacroDefinition macro in
                        tu.PreprocessedEntities.OfType<MacroDefinition>())
                {
                    if (macro.Enumeration != null)
                        continue;

                    Field item = GenerateConstValueFromMacro(context, macro);
                    if (item == null)
                        continue;
                    wrappingClass.Fields.Add(item);
                }
            }
        }

        private static bool GetCreateWrappingClass(string className, TranslationUnit tu, out Class wrappingClass)
        {
            wrappingClass = tu.FindClass(className);
            if (wrappingClass == null)
            {
                wrappingClass = new Class {Name = className, Namespace = tu, IsStatic = true};
                return true;
            }
            return false;
        }

        public static void MoveAllIntoWrapperClass(this FFmpegSubLibrary library,
            IEnumerable<TranslationUnit> ourTranslationUnits)
        {
            foreach (TranslationUnit tu in ourTranslationUnits)
            {
                Class wrappingClass;
                GetCreateWrappingClass(library.LibraryNameSpace, tu, out wrappingClass);

                wrappingClass.Classes.AddRange(tu.Classes.Except(new List<Class> {wrappingClass}));
                foreach (Class decl in wrappingClass.Classes)
                {
                    decl.Namespace = wrappingClass;
                }
                tu.Classes.Clear();
                tu.Classes.Add(wrappingClass);

                wrappingClass.Functions.AddRange(tu.Functions);
                foreach (Function decl in wrappingClass.Functions)
                {
                    decl.Namespace = wrappingClass;
                }
                tu.Functions.Clear();

                wrappingClass.Enums.AddRange(tu.Enums);
                foreach (Enumeration decl in wrappingClass.Enums)
                {
                    decl.Namespace = wrappingClass;
                }
                tu.Enums.Clear();
            }
        }
    }
}
using CppSharp;
using CppSharp.AST;

namespace FFmpegBindings
{
    internal class AVUtilLibrary : ILibrary
    {
        public void Setup(Driver driver)
        {
            DriverOptions options = driver.Options;
            options.LibraryName = "avutil_";
            options.OutputDir = @"C:\WORK\REPOS-SC\FFmpeg_bindings\generated\2.1.3";
            options.IncludeDirs.Add(@"C:\WORK\REPOS-SC\FFmpeg_bindings\ffmpeg\2.1.3");
            options.Headers.Add(@"libavutil\avutil.h");
        }

        public void SetupPasses(Driver driver)
        {
            //driver.TranslationUnitPasses.RemovePrefix("SDL_");
            //driver.TranslationUnitPasses.RemovePrefix("SCANCODE_");
            //driver.TranslationUnitPasses.RemovePrefix("SDLK_");
            //driver.TranslationUnitPasses.RemovePrefix("KMOD_");
            //driver.TranslationUnitPasses.RemovePrefix("LOG_CATEGORY_");
        }

        public void Preprocess(Driver driver, ASTContext ctx)
        {
            //ctx.IgnoreEnumWithMatchingItem("SDL_FALSE");
            //ctx.IgnoreEnumWithMatchingItem("DUMMY_ENUM_VALUE");

            //ctx.SetNameOfEnumWithMatchingItem("SDL_SCANCODE_UNKNOWN", "ScanCode");
            //ctx.SetNameOfEnumWithMatchingItem("SDLK_UNKNOWN", "Key");
            //ctx.SetNameOfEnumWithMatchingItem("KMOD_NONE", "KeyModifier");
            //ctx.SetNameOfEnumWithMatchingItem("SDL_LOG_CATEGORY_CUSTOM", "LogCategory");

            //ctx.GenerateEnumFromMacros("InitFlags", "SDL_INIT_(.*)").SetFlags();
            //ctx.GenerateEnumFromMacros("Endianness", "SDL_(.*)_ENDIAN");
            //ctx.GenerateEnumFromMacros("InputState", "SDL_RELEASED", "SDL_PRESSED");
            //ctx.GenerateEnumFromMacros("AlphaState", "SDL_ALPHA_(.*)");
            //ctx.GenerateEnumFromMacros("HatState", "SDL_HAT_(.*)");

            //ctx.IgnoreHeadersWithName("SDL_atomic*");
            //ctx.IgnoreHeadersWithName("SDL_endian*");
            //ctx.IgnoreHeadersWithName("SDL_main*");
            //ctx.IgnoreHeadersWithName("SDL_mutex*");
            //ctx.IgnoreHeadersWithName("SDL_stdinc*");
            //ctx.IgnoreHeadersWithName("SDL_error");

            //ctx.IgnoreEnumWithMatchingItem("SDL_ENOMEM");
            //ctx.IgnoreFunctionWithName("SDL_Error");
        }

        public void Postprocess(Driver driver, ASTContext lib)
        {
            //lib.SetNameOfEnumWithName("PIXELTYPE", "PixelType");
            //lib.SetNameOfEnumWithName("BITMAPORDER", "BitmapOrder");
            //lib.SetNameOfEnumWithName("PACKEDORDER", "PackedOrder");
            //lib.SetNameOfEnumWithName("ARRAYORDER", "ArrayOrder");
            //lib.SetNameOfEnumWithName("PACKEDLAYOUT", "PackedLayout");
            //lib.SetNameOfEnumWithName("PIXELFORMAT", "PixelFormats");
            //lib.SetNameOfEnumWithName("assert_state", "AssertState");
            //lib.SetClassBindName("assert_data", "AssertData");
            //lib.SetNameOfEnumWithName("eventaction", "EventAction");
            //lib.SetNameOfEnumWithName("LOG_CATEGORY", "LogCategory");
        }
    }
}
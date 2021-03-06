//----------------------------------------------------------------------------
// This is autogenerated code by CppSharp.
// Do not edit this file or all your changes will be lost after re-generation.
//----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace FFmpeg
{
    public unsafe static partial class libavutil
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct AVSHA
        {
        }

        /// <summary>
        /// Initialize SHA-1 or SHA-2 hashing.
        /// 
        /// @param context pointer to the function context (of size av_sha_size)
        /// @param bits    number of bits in digest (SHA-1 - 160 bits, SHA-2 224 or
        /// 256 bits)
        /// @return        zero if initialization succeeded, -1 otherwise
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_sha_init")]
        public static extern int av_sha_init(libavutil.AVSHA* context, int bits);

        /// <summary>
        /// Update hash value.
        /// 
        /// @param context hash function context
        /// @param data    input data to update hash with
        /// @param len     input data length
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_sha_update")]
        public static extern void av_sha_update(libavutil.AVSHA* context, byte* data, uint len);

        /// <summary>
        /// Finish hashing and output digest value.
        /// 
        /// @param context hash function context
        /// @param digest  buffer where output digest value is stored
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_sha_final")]
        public static extern void av_sha_final(libavutil.AVSHA* context, byte* digest);
    }
}

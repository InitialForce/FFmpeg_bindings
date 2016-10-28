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
        public unsafe partial struct AVRIPEMD
        {
        }

        /// <summary>
        /// Allocate an AVRIPEMD context.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_ripemd_alloc")]
        public static extern libavutil.AVRIPEMD* av_ripemd_alloc();

        /// <summary>
        /// Initialize RIPEMD hashing.
        /// </summary>
        /// <param name="context">
        /// pointer to the function context (of size av_ripemd_size)
        /// </param>
        /// <param name="bits">
        /// number of bits in digest (128, 160, 256 or 320 bits)
        /// </param>
        /// <returns>
        /// zero if initialization succeeded, -1 otherwise
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_ripemd_init")]
        public static extern int av_ripemd_init(libavutil.AVRIPEMD* context, int bits);

        /// <summary>
        /// Update hash value.
        /// </summary>
        /// <param name="context">
        /// hash function context
        /// </param>
        /// <param name="data">
        /// input data to update hash with
        /// </param>
        /// <param name="len">
        /// input data length
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_ripemd_update")]
        public static extern void av_ripemd_update(libavutil.AVRIPEMD* context, byte* data, uint len);

        /// <summary>
        /// Finish hashing and output digest value.
        /// </summary>
        /// <param name="context">
        /// hash function context
        /// </param>
        /// <param name="digest">
        /// buffer where output digest value is stored
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_ripemd_final")]
        public static extern void av_ripemd_final(libavutil.AVRIPEMD* context, byte* digest);
    }
}

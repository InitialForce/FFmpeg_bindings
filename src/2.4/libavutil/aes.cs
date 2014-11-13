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
        public unsafe partial struct AVAES
        {
        }

        /// <summary>
        /// Allocate an AVAES context.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_aes_alloc")]
        public static extern libavutil.AVAES* av_aes_alloc();

        /// <summary>
        /// Initialize an AVAES context.
        /// </summary>
        /// <param name="key_bits">
        /// 128, 192 or 256
        /// </param>
        /// <param name="decrypt">
        /// 0 for encryption, 1 for decryption
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_aes_init")]
        public static extern int av_aes_init(libavutil.AVAES* a, byte* key, int key_bits, int decrypt);

        /// <summary>
        /// Encrypt or decrypt a buffer using a previously initialized context.
        /// </summary>
        /// <param name="count">
        /// number of 16 byte blocks
        /// </param>
        /// <param name="dst">
        /// destination array, can be equal to src
        /// </param>
        /// <param name="src">
        /// source array, can be equal to dst
        /// </param>
        /// <param name="iv">
        /// initialization vector for CBC mode, if NULL then ECB will be used
        /// </param>
        /// <param name="decrypt">
        /// 0 for encryption, 1 for decryption
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_aes_crypt")]
        public static extern void av_aes_crypt(libavutil.AVAES* a, byte* dst, byte* src, int count, byte* iv, int decrypt);
    }
}
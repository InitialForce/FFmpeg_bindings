//----------------------------------------------------------------------------
// This is autogenerated code by CppSharp.
// Do not edit this file or all your changes will be lost after re-generation.
//----------------------------------------------------------------------------
// ReSharper disable RedundantUsingDirective
// ReSharper disable CheckNamespace
#pragma warning disable 1584,1711,1572,1581,1580,1573
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace FFmpeg
{
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "UnusedMember.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "InconsistentNaming")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "RedundantUnsafeContext")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "MemberCanBePrivate.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "MemberCanBePrivate.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "PartialTypeWithSinglePart")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "RedundantNameQualifier")]
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("ReSharper", "ArrangeModifiersOrder")]
    public unsafe static partial class libavutil
    {
        /// <summary>
        /// @file
        /// @brief Public header for libavutil XTEA algorithm
        /// @defgroup lavu_xtea XTEA
        /// @ingroup lavu_crypto
        /// @{
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct AVXTEA
        {
            public fixed uint key[16];
        }

        /// <summary>
        /// Allocate an AVXTEA context.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_xtea_alloc")]
        public static extern libavutil.AVXTEA* av_xtea_alloc();

        /// <summary>
        /// Initialize an AVXTEA context.
        /// </summary>
        /// <param name="ctx">
        /// an AVXTEA context
        /// </param>
        /// <param name="key">
        /// a key of 16 bytes used for encryption/decryption,
        /// interpreted as big endian 32 bit numbers
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_xtea_init")]
        public static extern void av_xtea_init(libavutil.AVXTEA* ctx, byte* key);

        /// <summary>
        /// Initialize an AVXTEA context.
        /// </summary>
        /// <param name="ctx">
        /// an AVXTEA context
        /// </param>
        /// <param name="key">
        /// a key of 16 bytes used for encryption/decryption,
        /// interpreted as little endian 32 bit numbers
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_xtea_le_init")]
        public static extern void av_xtea_le_init(libavutil.AVXTEA* ctx, byte* key);

        /// <summary>
        /// Encrypt or decrypt a buffer using a previously initialized context,
        /// in big endian format.
        /// </summary>
        /// <param name="ctx">
        /// an AVXTEA context
        /// </param>
        /// <param name="dst">
        /// destination array, can be equal to src
        /// </param>
        /// <param name="src">
        /// source array, can be equal to dst
        /// </param>
        /// <param name="count">
        /// number of 8 byte blocks
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
            EntryPoint="av_xtea_crypt")]
        public static extern void av_xtea_crypt(libavutil.AVXTEA* ctx, byte* dst, byte* src, int count, byte* iv, int decrypt);

        /// <summary>
        /// Encrypt or decrypt a buffer using a previously initialized context,
        /// in little endian format.
        /// </summary>
        /// <param name="ctx">
        /// an AVXTEA context
        /// </param>
        /// <param name="dst">
        /// destination array, can be equal to src
        /// </param>
        /// <param name="src">
        /// source array, can be equal to dst
        /// </param>
        /// <param name="count">
        /// number of 8 byte blocks
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
            EntryPoint="av_xtea_le_crypt")]
        public static extern void av_xtea_le_crypt(libavutil.AVXTEA* ctx, byte* dst, byte* src, int count, byte* iv, int decrypt);
    }
}

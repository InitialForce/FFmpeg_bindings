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
        public const sbyte AV_LZO_INPUT_DEPLETED = 1;

        public const sbyte AV_LZO_OUTPUT_FULL = 2;

        public const sbyte AV_LZO_INVALID_BACKPTR = 4;

        public const sbyte AV_LZO_ERROR = 8;

        public const sbyte AV_LZO_INPUT_PADDING = 8;

        public const sbyte AV_LZO_OUTPUT_PADDING = 12;

        /// <summary>
        /// @brief Decodes LZO 1x compressed data.
        /// @param out output buffer
        /// @param outlen size of output buffer, number of bytes left are returned
        /// here
        /// @param in input buffer
        /// @param inlen size of input buffer, number of bytes left are returned
        /// here
        /// @return 0 on success, otherwise a combination of the error flags above
        /// 
        /// Make sure all buffers are appropriately padded, in must provide
        /// AV_LZO_INPUT_PADDING, out must provide AV_LZO_OUTPUT_PADDING additional
        /// bytes.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_lzo1x_decode")]
        public static extern int av_lzo1x_decode(void* _out, int* outlen, void* _in, int* inlen);
    }
}
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
        /// <summary>
        /// Read the file with name filename, and put its content in a newly
        /// allocated buffer or map it with mmap() when available.
        /// In case of success set *bufptr to the read or mmapped buffer, and
        /// size to the size in bytes of the buffer in *bufptr.
        /// The returned buffer must be released with av_file_unmap().
        /// </summary>
        /// <param name="log_offset">
        /// loglevel offset used for logging
        /// </param>
        /// <param name="log_ctx">
        /// context used for logging
        /// </param>
        /// <returns>
        /// a non negative number in case of success, a negative value
        /// corresponding to an AVERROR error code in case of failure
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_file_map")]
        public static extern int av_file_map(string filename, byte** bufptr, global::System.UIntPtr* size, int log_offset, void* log_ctx);

        /// <summary>
        /// Read the file with name filename, and put its content in a newly
        /// allocated buffer or map it with mmap() when available.
        /// In case of success set *bufptr to the read or mmapped buffer, and
        /// size to the size in bytes of the buffer in *bufptr.
        /// The returned buffer must be released with av_file_unmap().
        /// </summary>
        /// <param name="log_offset">
        /// loglevel offset used for logging
        /// </param>
        /// <param name="log_ctx">
        /// context used for logging
        /// </param>
        /// <returns>
        /// a non negative number in case of success, a negative value
        /// corresponding to an AVERROR error code in case of failure
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_file_map")]
        public static extern int av_file_map(string filename, ref byte* bufptr, global::System.UIntPtr* size, int log_offset, void* log_ctx);

        /// <summary>
        /// Unmap or free the buffer bufptr created by av_file_map().
        /// </summary>
        /// <param name="size">
        /// size in bytes of bufptr, must be the same as returned
        /// by av_file_map()
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_file_unmap")]
        public static extern void av_file_unmap(byte* bufptr, global::System.UIntPtr size);

        /// <summary>
        /// Wrapper to work around the lack of mkstemp() on mingw.
        /// Also, tries to create file in /tmp first, if possible.
        /// prefix can be a character constant; *filename will be allocated
        /// internally.
        /// </summary>
        /// <returns>
        /// file descriptor of opened file (or -1 on error)
        /// and opened file name in **filename.
        /// </returns>
        /// <remark>
        /// On very old libcs it is necessary to set a secure umask before
        /// calling this, av_tempfile() can't call umask itself as it is used in
        /// libraries and could interfere with the calling application.
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_tempfile")]
        public static extern int av_tempfile(string prefix, sbyte** filename, int log_offset, void* log_ctx);

        /// <summary>
        /// Wrapper to work around the lack of mkstemp() on mingw.
        /// Also, tries to create file in /tmp first, if possible.
        /// prefix can be a character constant; *filename will be allocated
        /// internally.
        /// </summary>
        /// <returns>
        /// file descriptor of opened file (or -1 on error)
        /// and opened file name in **filename.
        /// </returns>
        /// <remark>
        /// On very old libcs it is necessary to set a secure umask before
        /// calling this, av_tempfile() can't call umask itself as it is used in
        /// libraries and could interfere with the calling application.
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_tempfile")]
        public static extern int av_tempfile(string prefix, ref System.Text.StringBuilder filename, int log_offset, void* log_ctx);
    }
}

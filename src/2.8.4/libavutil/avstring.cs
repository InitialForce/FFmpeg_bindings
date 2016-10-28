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
        public const sbyte AV_ESCAPE_FLAG_WHITESPACE = 0x1;

        public const sbyte AV_ESCAPE_FLAG_STRICT = 0x2;

        public const sbyte AV_UTF8_FLAG_ACCEPT_INVALID_BIG_CODES = 1;

        public const sbyte AV_UTF8_FLAG_ACCEPT_NON_CHARACTERS = 2;

        public const sbyte AV_UTF8_FLAG_ACCEPT_SURROGATES = 4;

        public const sbyte AV_UTF8_FLAG_EXCLUDE_XML_INVALID_CONTROL_CODES = 8;

        public enum AVEscapeMode
        {
            /// <summary>Use auto-selected escaping mode.</summary>
            AV_ESCAPE_MODE_AUTO = 0,
            /// <summary>Use backslash escaping.</summary>
            AV_ESCAPE_MODE_BACKSLASH = 1,
            /// <summary>Use single-quote escaping.</summary>
            AV_ESCAPE_MODE_QUOTE = 2
        }

        /// <summary>
        /// Return non-zero if pfx is a prefix of str. If it is, *ptr is set to
        /// the address of the first character in str after the prefix.
        /// </summary>
        /// <param name="str">
        /// input string
        /// </param>
        /// <param name="pfx">
        /// prefix to test
        /// </param>
        /// <param name="ptr">
        /// updated if the prefix is matched inside str
        /// </param>
        /// <returns>
        /// non-zero if the prefix matches, zero otherwise
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strstart")]
        public static extern int av_strstart(string str, string pfx, sbyte** ptr);

        /// <summary>
        /// Return non-zero if pfx is a prefix of str. If it is, *ptr is set to
        /// the address of the first character in str after the prefix.
        /// </summary>
        /// <param name="str">
        /// input string
        /// </param>
        /// <param name="pfx">
        /// prefix to test
        /// </param>
        /// <param name="ptr">
        /// updated if the prefix is matched inside str
        /// </param>
        /// <returns>
        /// non-zero if the prefix matches, zero otherwise
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strstart")]
        public static extern int av_strstart(string str, string pfx, ref string ptr);

        /// <summary>
        /// Return non-zero if pfx is a prefix of str independent of case. If
        /// it is, *ptr is set to the address of the first character in str
        /// after the prefix.
        /// </summary>
        /// <param name="str">
        /// input string
        /// </param>
        /// <param name="pfx">
        /// prefix to test
        /// </param>
        /// <param name="ptr">
        /// updated if the prefix is matched inside str
        /// </param>
        /// <returns>
        /// non-zero if the prefix matches, zero otherwise
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_stristart")]
        public static extern int av_stristart(string str, string pfx, sbyte** ptr);

        /// <summary>
        /// Return non-zero if pfx is a prefix of str independent of case. If
        /// it is, *ptr is set to the address of the first character in str
        /// after the prefix.
        /// </summary>
        /// <param name="str">
        /// input string
        /// </param>
        /// <param name="pfx">
        /// prefix to test
        /// </param>
        /// <param name="ptr">
        /// updated if the prefix is matched inside str
        /// </param>
        /// <returns>
        /// non-zero if the prefix matches, zero otherwise
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_stristart")]
        public static extern int av_stristart(string str, string pfx, ref string ptr);

        /// <summary>
        /// Locate the first case-independent occurrence in the string haystack
        /// of the string needle.  A zero-length string needle is considered to
        /// match at the start of haystack.
        /// 
        /// This function is a case-insensitive version of the standard strstr().
        /// </summary>
        /// <param name="haystack">
        /// string to search in
        /// </param>
        /// <param name="needle">
        /// string to search for
        /// </param>
        /// <returns>
        /// pointer to the located match within haystack
        /// or a null pointer if no match
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_stristr")]
        public static extern sbyte* av_stristr(string haystack, string needle);

        /// <summary>
        /// Locate the first occurrence of the string needle in the string haystack
        /// where not more than hay_length characters are searched. A zero-length
        /// string needle is considered to match at the start of haystack.
        /// 
        /// This function is a length-limited version of the standard strstr().
        /// </summary>
        /// <param name="haystack">
        /// string to search in
        /// </param>
        /// <param name="needle">
        /// string to search for
        /// </param>
        /// <param name="hay_length">
        /// length of string to search in
        /// </param>
        /// <returns>
        /// pointer to the located match within haystack
        /// or a null pointer if no match
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strnstr")]
        public static extern sbyte* av_strnstr(string haystack, string needle, global::System.UIntPtr hay_length);

        /// <summary>
        /// Copy the string src to dst, but no more than size - 1 bytes, and
        /// null-terminate dst.
        /// 
        /// This function is the same as BSD strlcpy().
        /// </summary>
        /// <param name="dst">
        /// destination buffer
        /// </param>
        /// <param name="src">
        /// source string
        /// </param>
        /// <param name="size">
        /// size of destination buffer
        /// </param>
        /// <returns>
        /// the length of src
        /// 
        /// @warning since the return value is the length of src, src absolutely
        /// _must_ be a properly 0-terminated string, otherwise this will read
        /// beyond
        /// the end of the buffer and possibly crash.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strlcpy")]
        public static extern global::System.UIntPtr av_strlcpy(System.Text.StringBuilder dst, string src, global::System.UIntPtr size);

        /// <summary>
        /// Append the string src to the string dst, but to a total length of
        /// no more than size - 1 bytes, and null-terminate dst.
        /// 
        /// This function is similar to BSD strlcat(), but differs when
        /// size <= strlen(dst).
        /// </summary>
        /// <param name="dst">
        /// destination buffer
        /// </param>
        /// <param name="src">
        /// source string
        /// </param>
        /// <param name="size">
        /// size of destination buffer
        /// </param>
        /// <returns>
        /// the total length of src and dst
        /// 
        /// @warning since the return value use the length of src and dst, these
        /// absolutely _must_ be a properly 0-terminated strings, otherwise this
        /// will read beyond the end of the buffer and possibly crash.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strlcat")]
        public static extern global::System.UIntPtr av_strlcat(System.Text.StringBuilder dst, string src, global::System.UIntPtr size);

        /// <summary>
        /// Append output to a string, according to a format. Never write out of
        /// the destination buffer, and always put a terminating 0 within
        /// the buffer.
        /// </summary>
        /// <param name="dst">
        /// destination buffer (string to which the output is
        /// appended)
        /// </param>
        /// <param name="size">
        /// total size of the destination buffer
        /// </param>
        /// <param name="fmt">
        /// printf-compatible format string, specifying how the
        /// following parameters are used
        /// </param>
        /// <returns>
        /// the length of the string that would have been generated
        /// if enough space had been available
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strlcatf")]
        public static extern global::System.UIntPtr av_strlcatf(System.Text.StringBuilder dst, global::System.UIntPtr size, string fmt);

        /// <summary>
        /// Get the count of continuous non zero chars starting from the beginning.
        /// </summary>
        /// <param name="len">
        /// maximum number of characters to check in the string, that
        /// is the maximum value which is returned by the function
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strnlen")]
        public static extern global::System.UIntPtr av_strnlen(string s, global::System.UIntPtr len);

        /// <summary>
        /// Print arguments following specified format into a large enough auto
        /// allocated buffer. It is similar to GNU asprintf().
        /// </summary>
        /// <param name="fmt">
        /// printf-compatible format string, specifying how the
        /// following parameters are used.
        /// </param>
        /// <returns>
        /// the allocated string
        /// </returns>
        /// <remark>
        /// You have to free the string yourself with av_free().
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_asprintf")]
        public static extern sbyte* av_asprintf(string fmt);

        /// <summary>
        /// Convert a number to a av_malloced string.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_d2str")]
        public static extern sbyte* av_d2str(double d);

        /// <summary>
        /// Unescape the given string until a non escaped terminating char,
        /// and return the token corresponding to the unescaped string.
        /// 
        /// The normal \ and ' escaping is supported. Leading and trailing
        /// whitespaces are removed, unless they are escaped with '\' or are
        /// enclosed between ''.
        /// </summary>
        /// <param name="buf">
        /// the buffer to parse, buf will be updated to point to the
        /// terminating char
        /// </param>
        /// <param name="term">
        /// a 0-terminated list of terminating chars
        /// </param>
        /// <returns>
        /// the malloced unescaped string, which must be av_freed by
        /// the user, NULL in case of allocation failure
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_get_token")]
        public static extern sbyte* av_get_token(sbyte** buf, string term);

        /// <summary>
        /// Unescape the given string until a non escaped terminating char,
        /// and return the token corresponding to the unescaped string.
        /// 
        /// The normal \ and ' escaping is supported. Leading and trailing
        /// whitespaces are removed, unless they are escaped with '\' or are
        /// enclosed between ''.
        /// </summary>
        /// <param name="buf">
        /// the buffer to parse, buf will be updated to point to the
        /// terminating char
        /// </param>
        /// <param name="term">
        /// a 0-terminated list of terminating chars
        /// </param>
        /// <returns>
        /// the malloced unescaped string, which must be av_freed by
        /// the user, NULL in case of allocation failure
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_get_token")]
        public static extern sbyte* av_get_token(ref string buf, string term);

        /// <summary>
        /// Split the string into several tokens which can be accessed by
        /// successive calls to av_strtok().
        /// 
        /// A token is defined as a sequence of characters not belonging to the
        /// set specified in delim.
        /// 
        /// On the first call to av_strtok(), s should point to the string to
        /// parse, and the value of saveptr is ignored. In subsequent calls, s
        /// should be NULL, and saveptr should be unchanged since the previous
        /// call.
        /// 
        /// This function is similar to strtok_r() defined in POSIX.1.
        /// </summary>
        /// <param name="s">
        /// the string to parse, may be NULL
        /// </param>
        /// <param name="delim">
        /// 0-terminated list of token delimiters, must be non-NULL
        /// </param>
        /// <param name="saveptr">
        /// user-provided pointer which points to stored
        /// information necessary for av_strtok() to continue scanning the same
        /// string. saveptr is updated to point to the next character after the
        /// first delimiter found, or to NULL if the string was terminated
        /// </param>
        /// <returns>
        /// the found token, or NULL when no token is found
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strtok")]
        public static extern sbyte* av_strtok(System.Text.StringBuilder s, string delim, sbyte** saveptr);

        /// <summary>
        /// Split the string into several tokens which can be accessed by
        /// successive calls to av_strtok().
        /// 
        /// A token is defined as a sequence of characters not belonging to the
        /// set specified in delim.
        /// 
        /// On the first call to av_strtok(), s should point to the string to
        /// parse, and the value of saveptr is ignored. In subsequent calls, s
        /// should be NULL, and saveptr should be unchanged since the previous
        /// call.
        /// 
        /// This function is similar to strtok_r() defined in POSIX.1.
        /// </summary>
        /// <param name="s">
        /// the string to parse, may be NULL
        /// </param>
        /// <param name="delim">
        /// 0-terminated list of token delimiters, must be non-NULL
        /// </param>
        /// <param name="saveptr">
        /// user-provided pointer which points to stored
        /// information necessary for av_strtok() to continue scanning the same
        /// string. saveptr is updated to point to the next character after the
        /// first delimiter found, or to NULL if the string was terminated
        /// </param>
        /// <returns>
        /// the found token, or NULL when no token is found
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strtok")]
        public static extern sbyte* av_strtok(System.Text.StringBuilder s, string delim, ref System.Text.StringBuilder saveptr);

        /// <summary>
        /// Locale-independent conversion of ASCII isdigit.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_isdigit")]
        public static extern int av_isdigit(int c);

        /// <summary>
        /// Locale-independent conversion of ASCII isgraph.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_isgraph")]
        public static extern int av_isgraph(int c);

        /// <summary>
        /// Locale-independent conversion of ASCII isspace.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_isspace")]
        public static extern int av_isspace(int c);

        /// <summary>
        /// Locale-independent conversion of ASCII characters to uppercase.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_toupper")]
        public static extern int av_toupper(int c);

        /// <summary>
        /// Locale-independent conversion of ASCII characters to lowercase.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_tolower")]
        public static extern int av_tolower(int c);

        /// <summary>
        /// Locale-independent conversion of ASCII isxdigit.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_isxdigit")]
        public static extern int av_isxdigit(int c);

        /// <summary>
        /// Locale-independent case-insensitive compare.
        /// </summary>
        /// <remark>
        /// This means only ASCII-range characters are case-insensitive
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strcasecmp")]
        public static extern int av_strcasecmp(string a, string b);

        /// <summary>
        /// Locale-independent case-insensitive compare.
        /// </summary>
        /// <remark>
        /// This means only ASCII-range characters are case-insensitive
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_strncasecmp")]
        public static extern int av_strncasecmp(string a, string b, global::System.UIntPtr n);

        /// <summary>
        /// Thread safe basename.
        /// </summary>
        /// <param name="path">
        /// the path, on DOS both \ and / are considered separators.
        /// </param>
        /// <returns>
        /// pointer to the basename substring.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_basename")]
        public static extern sbyte* av_basename(string path);

        /// <summary>
        /// Thread safe dirname.
        /// </summary>
        /// <param name="path">
        /// the path, on DOS both \ and / are considered separators.
        /// </param>
        /// <returns>
        /// the path with the separator replaced by the string terminator or ".".
        /// </returns>
        /// <remark>
        /// the function may change the input string.
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dirname")]
        public static extern sbyte* av_dirname(System.Text.StringBuilder path);

        /// <summary>
        /// Match instances of a name in a comma-separated list of names.
        /// </summary>
        /// <param name="name">
        /// Name to look for.
        /// </param>
        /// <param name="names">
        /// List of names.
        /// </param>
        /// <returns>
        /// 1 on match, 0 otherwise.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_match_name")]
        public static extern int av_match_name(string name, string names);

        /// <summary>
        /// Append path component to the existing path.
        /// Path separator '/' is placed between when needed.
        /// Resulting string have to be freed with av_free().
        /// </summary>
        /// <param name="path">
        /// base path
        /// </param>
        /// <param name="component">
        /// component to be appended
        /// </param>
        /// <returns>
        /// new path or NULL on error.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_append_path_component")]
        public static extern sbyte* av_append_path_component(string path, string component);

        /// <summary>
        /// Escape string in src, and put the escaped string in an allocated
        /// string in *dst, which must be freed with av_free().
        /// </summary>
        /// <param name="dst">
        /// pointer where an allocated string is put
        /// </param>
        /// <param name="src">
        /// string to escape, must be non-NULL
        /// </param>
        /// <param name="special_chars">
        /// string containing the special characters which
        /// need to be escaped, can be NULL
        /// </param>
        /// <param name="mode">
        /// escape mode to employ, see AV_ESCAPE_MODE_* macros.
        /// Any unknown value for mode will be considered equivalent to
        /// AV_ESCAPE_MODE_BACKSLASH, but this behaviour can change without
        /// notice.
        /// </param>
        /// <param name="flags">
        /// flags which control how to escape, see AV_ESCAPE_FLAG_ macros
        /// </param>
        /// <returns>
        /// the length of the allocated string, or a negative error code in case of
        /// error
        /// @see av_bprint_escape()
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_escape")]
        public static extern int av_escape(sbyte** dst, string src, string special_chars, libavutil.AVEscapeMode mode, int flags);

        /// <summary>
        /// Escape string in src, and put the escaped string in an allocated
        /// string in *dst, which must be freed with av_free().
        /// </summary>
        /// <param name="dst">
        /// pointer where an allocated string is put
        /// </param>
        /// <param name="src">
        /// string to escape, must be non-NULL
        /// </param>
        /// <param name="special_chars">
        /// string containing the special characters which
        /// need to be escaped, can be NULL
        /// </param>
        /// <param name="mode">
        /// escape mode to employ, see AV_ESCAPE_MODE_* macros.
        /// Any unknown value for mode will be considered equivalent to
        /// AV_ESCAPE_MODE_BACKSLASH, but this behaviour can change without
        /// notice.
        /// </param>
        /// <param name="flags">
        /// flags which control how to escape, see AV_ESCAPE_FLAG_ macros
        /// </param>
        /// <returns>
        /// the length of the allocated string, or a negative error code in case of
        /// error
        /// @see av_bprint_escape()
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_escape")]
        public static extern int av_escape(ref System.Text.StringBuilder dst, string src, string special_chars, libavutil.AVEscapeMode mode, int flags);

        /// <summary>
        /// Read and decode a single UTF-8 code point (character) from the
        /// buffer in *buf, and update *buf to point to the next byte to
        /// decode.
        /// 
        /// In case of an invalid byte sequence, the pointer will be updated to
        /// the next byte after the invalid sequence and the function will
        /// return an error code.
        /// 
        /// Depending on the specified flags, the function will also fail in
        /// case the decoded code point does not belong to a valid range.
        /// </summary>
        /// <remark>
        /// For speed-relevant code a carefully implemented use of
        /// GET_UTF8() may be preferred.
        /// </remark>
        /// <param name="codep">
        /// pointer used to return the parsed code in case of success.
        /// The value in *codep is set even in case the range check fails.
        /// </param>
        /// <param name="bufp">
        /// pointer to the address the first byte of the sequence
        /// to decode, updated by the function to point to the
        /// byte next after the decoded sequence
        /// </param>
        /// <param name="buf_end">
        /// pointer to the end of the buffer, points to the next
        /// byte past the last in the buffer. This is used to
        /// avoid buffer overreads (in case of an unfinished
        /// UTF-8 sequence towards the end of the buffer).
        /// </param>
        /// <param name="flags">
        /// a collection of AV_UTF8_FLAG_* flags
        /// </param>
        /// <returns>
        /// >= 0 in case a sequence was successfully read, a negative
        /// value in case of invalid sequence
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_utf8_decode")]
        public static extern int av_utf8_decode(int* codep, byte** bufp, byte* buf_end, uint flags);

        /// <summary>
        /// Read and decode a single UTF-8 code point (character) from the
        /// buffer in *buf, and update *buf to point to the next byte to
        /// decode.
        /// 
        /// In case of an invalid byte sequence, the pointer will be updated to
        /// the next byte after the invalid sequence and the function will
        /// return an error code.
        /// 
        /// Depending on the specified flags, the function will also fail in
        /// case the decoded code point does not belong to a valid range.
        /// </summary>
        /// <remark>
        /// For speed-relevant code a carefully implemented use of
        /// GET_UTF8() may be preferred.
        /// </remark>
        /// <param name="codep">
        /// pointer used to return the parsed code in case of success.
        /// The value in *codep is set even in case the range check fails.
        /// </param>
        /// <param name="bufp">
        /// pointer to the address the first byte of the sequence
        /// to decode, updated by the function to point to the
        /// byte next after the decoded sequence
        /// </param>
        /// <param name="buf_end">
        /// pointer to the end of the buffer, points to the next
        /// byte past the last in the buffer. This is used to
        /// avoid buffer overreads (in case of an unfinished
        /// UTF-8 sequence towards the end of the buffer).
        /// </param>
        /// <param name="flags">
        /// a collection of AV_UTF8_FLAG_* flags
        /// </param>
        /// <returns>
        /// >= 0 in case a sequence was successfully read, a negative
        /// value in case of invalid sequence
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_utf8_decode")]
        public static extern int av_utf8_decode(int* codep, ref byte* bufp, byte* buf_end, uint flags);

        /// <summary>
        /// Check if a name is in a list.
        /// </summary>
        /// <returns>
        /// s 0 if not found, or the 1 based index where it has been found in the
        /// list.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_match_list")]
        public static extern int av_match_list(string name, string list, sbyte separator);
    }
}

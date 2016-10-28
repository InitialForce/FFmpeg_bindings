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
        public const sbyte AV_DICT_MATCH_CASE = 1;

        public const sbyte AV_DICT_IGNORE_SUFFIX = 2;

        public const sbyte AV_DICT_DONT_STRDUP_KEY = 4;

        public const sbyte AV_DICT_DONT_STRDUP_VAL = 8;

        public const sbyte AV_DICT_DONT_OVERWRITE = 16;

        public const sbyte AV_DICT_APPEND = 32;

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct AVDictionary
        {
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct AVDictionaryEntry
        {
            public sbyte* key;

            public sbyte* value;
        }

        /// <summary>
        /// Get a dictionary entry with matching key.
        /// 
        /// The returned entry key or value must not be changed, or it will
        /// cause undefined behavior.
        /// 
        /// To iterate through all the dictionary entries, you can set the matching
        /// key
        /// to the null string "" and set the AV_DICT_IGNORE_SUFFIX flag.
        /// </summary>
        /// <param name="prev">
        /// Set to the previous matching element to find the next.
        /// If set to NULL the first matching element is returned.
        /// </param>
        /// <param name="key">
        /// matching key
        /// </param>
        /// <param name="flags">
        /// a collection of AV_DICT_* flags controlling how the entry is retrieved
        /// </param>
        /// <returns>
        /// found entry or NULL in case no matching entry was found in the
        /// dictionary
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_get")]
        public static extern libavutil.AVDictionaryEntry* av_dict_get(libavutil.AVDictionary* m, string key, libavutil.AVDictionaryEntry* prev, int flags);

        /// <summary>
        /// Get number of entries in dictionary.
        /// </summary>
        /// <param name="m">
        /// dictionary
        /// </param>
        /// <returns>
        /// number of entries in dictionary
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_count")]
        public static extern int av_dict_count(libavutil.AVDictionary* m);

        /// <summary>
        /// Set the given entry in *pm, overwriting an existing entry.
        /// 
        /// Note: If AV_DICT_DONT_STRDUP_KEY or AV_DICT_DONT_STRDUP_VAL is set,
        /// these arguments will be freed on error.
        /// </summary>
        /// <param name="pm">
        /// pointer to a pointer to a dictionary struct. If *pm is NULL
        /// a dictionary struct is allocated and put in *pm.
        /// </param>
        /// <param name="key">
        /// entry key to add to *pm (will be av_strduped depending on flags)
        /// </param>
        /// <param name="value">
        /// entry value to add to *pm (will be av_strduped depending on flags).
        /// Passing a NULL value will cause an existing entry to be deleted.
        /// </param>
        /// <returns>
        /// >= 0 on success otherwise an error code <0
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_set")]
        public static extern int av_dict_set(libavutil.AVDictionary** pm, string key, string value, int flags);

        /// <summary>
        /// Set the given entry in *pm, overwriting an existing entry.
        /// 
        /// Note: If AV_DICT_DONT_STRDUP_KEY or AV_DICT_DONT_STRDUP_VAL is set,
        /// these arguments will be freed on error.
        /// </summary>
        /// <param name="pm">
        /// pointer to a pointer to a dictionary struct. If *pm is NULL
        /// a dictionary struct is allocated and put in *pm.
        /// </param>
        /// <param name="key">
        /// entry key to add to *pm (will be av_strduped depending on flags)
        /// </param>
        /// <param name="value">
        /// entry value to add to *pm (will be av_strduped depending on flags).
        /// Passing a NULL value will cause an existing entry to be deleted.
        /// </param>
        /// <returns>
        /// >= 0 on success otherwise an error code <0
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_set")]
        public static extern int av_dict_set(ref libavutil.AVDictionary* pm, string key, string value, int flags);

        /// <summary>
        /// Convenience wrapper for av_dict_set that converts the value to a string
        /// and stores it.
        /// 
        /// Note: If AV_DICT_DONT_STRDUP_KEY is set, key will be freed on error.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_set_int")]
        public static extern int av_dict_set_int(libavutil.AVDictionary** pm, string key, long value, int flags);

        /// <summary>
        /// Convenience wrapper for av_dict_set that converts the value to a string
        /// and stores it.
        /// 
        /// Note: If AV_DICT_DONT_STRDUP_KEY is set, key will be freed on error.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_set_int")]
        public static extern int av_dict_set_int(ref libavutil.AVDictionary* pm, string key, long value, int flags);

        /// <summary>
        /// Parse the key/value pairs list and add the parsed entries to a
        /// dictionary.
        /// 
        /// In case of failure, all the successfully set entries are stored in
        /// pm. You may need to manually free the created dictionary.
        /// </summary>
        /// <param name="key_val_sep">
        /// a 0-terminated list of characters used to separate
        /// key from value
        /// </param>
        /// <param name="pairs_sep">
        /// a 0-terminated list of characters used to separate
        /// two pairs from each other
        /// </param>
        /// <param name="flags">
        /// flags to use when adding to dictionary.
        /// AV_DICT_DONT_STRDUP_KEY and AV_DICT_DONT_STRDUP_VAL
        /// are ignored since the key/value tokens will always
        /// be duplicated.
        /// </param>
        /// <returns>
        /// 0 on success, negative AVERROR code on failure
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_parse_string")]
        public static extern int av_dict_parse_string(libavutil.AVDictionary** pm, string str, string key_val_sep, string pairs_sep, int flags);

        /// <summary>
        /// Parse the key/value pairs list and add the parsed entries to a
        /// dictionary.
        /// 
        /// In case of failure, all the successfully set entries are stored in
        /// pm. You may need to manually free the created dictionary.
        /// </summary>
        /// <param name="key_val_sep">
        /// a 0-terminated list of characters used to separate
        /// key from value
        /// </param>
        /// <param name="pairs_sep">
        /// a 0-terminated list of characters used to separate
        /// two pairs from each other
        /// </param>
        /// <param name="flags">
        /// flags to use when adding to dictionary.
        /// AV_DICT_DONT_STRDUP_KEY and AV_DICT_DONT_STRDUP_VAL
        /// are ignored since the key/value tokens will always
        /// be duplicated.
        /// </param>
        /// <returns>
        /// 0 on success, negative AVERROR code on failure
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_parse_string")]
        public static extern int av_dict_parse_string(ref libavutil.AVDictionary* pm, string str, string key_val_sep, string pairs_sep, int flags);

        /// <summary>
        /// Copy entries from one AVDictionary struct into another.
        /// </summary>
        /// <param name="dst">
        /// pointer to a pointer to a AVDictionary struct. If *dst is NULL,
        /// this function will allocate a struct for you and put it in *dst
        /// </param>
        /// <param name="src">
        /// pointer to source AVDictionary struct
        /// </param>
        /// <param name="flags">
        /// flags to use when setting entries in *dst
        /// </param>
        /// <remark>
        /// metadata is read using the AV_DICT_IGNORE_SUFFIX flag
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_copy")]
        public static extern void av_dict_copy(libavutil.AVDictionary** dst, libavutil.AVDictionary* src, int flags);

        /// <summary>
        /// Copy entries from one AVDictionary struct into another.
        /// </summary>
        /// <param name="dst">
        /// pointer to a pointer to a AVDictionary struct. If *dst is NULL,
        /// this function will allocate a struct for you and put it in *dst
        /// </param>
        /// <param name="src">
        /// pointer to source AVDictionary struct
        /// </param>
        /// <param name="flags">
        /// flags to use when setting entries in *dst
        /// </param>
        /// <remark>
        /// metadata is read using the AV_DICT_IGNORE_SUFFIX flag
        /// </remark>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_copy")]
        public static extern void av_dict_copy(ref libavutil.AVDictionary* dst, libavutil.AVDictionary* src, int flags);

        /// <summary>
        /// Free all the memory allocated for an AVDictionary struct
        /// and all keys and values.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_free")]
        public static extern void av_dict_free(libavutil.AVDictionary** m);

        /// <summary>
        /// Free all the memory allocated for an AVDictionary struct
        /// and all keys and values.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_free")]
        public static extern void av_dict_free(ref libavutil.AVDictionary* m);

        /// <summary>
        /// Get dictionary entries as a string.
        /// 
        /// Create a string containing dictionary's entries.
        /// Such string may be passed back to av_dict_parse_string().
        /// </summary>
        /// <remark>
        /// String is escaped with backslashes ('\').
        /// </remark>
        /// <param name="[in]">
        /// m             dictionary
        /// </param>
        /// <param name="[out]">
        /// buffer        Pointer to buffer that will be allocated with string
        /// containg entries.
        /// Buffer must be freed by the caller when is no longer needed.
        /// </param>
        /// <param name="[in]">
        /// key_val_sep   character used to separate key from value
        /// </param>
        /// <param name="[in]">
        /// pairs_sep     character used to separate two pairs from each other
        /// </param>
        /// <returns>
        /// >= 0 on success, negative on error
        /// @warning Separators cannot be neither '\\' nor '\0'. They also cannot
        /// be the same.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_get_string")]
        public static extern int av_dict_get_string(libavutil.AVDictionary* m, sbyte** buffer, sbyte key_val_sep, sbyte pairs_sep);

        /// <summary>
        /// Get dictionary entries as a string.
        /// 
        /// Create a string containing dictionary's entries.
        /// Such string may be passed back to av_dict_parse_string().
        /// </summary>
        /// <remark>
        /// String is escaped with backslashes ('\').
        /// </remark>
        /// <param name="[in]">
        /// m             dictionary
        /// </param>
        /// <param name="[out]">
        /// buffer        Pointer to buffer that will be allocated with string
        /// containg entries.
        /// Buffer must be freed by the caller when is no longer needed.
        /// </param>
        /// <param name="[in]">
        /// key_val_sep   character used to separate key from value
        /// </param>
        /// <param name="[in]">
        /// pairs_sep     character used to separate two pairs from each other
        /// </param>
        /// <returns>
        /// >= 0 on success, negative on error
        /// @warning Separators cannot be neither '\\' nor '\0'. They also cannot
        /// be the same.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_dict_get_string")]
        public static extern int av_dict_get_string(libavutil.AVDictionary* m, ref System.Text.StringBuilder buffer, sbyte key_val_sep, sbyte pairs_sep);
    }
}

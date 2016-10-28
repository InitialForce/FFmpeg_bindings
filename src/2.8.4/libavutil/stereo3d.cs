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
        public const ulong AV_STEREO3D_FLAG_INVERT = (1 << 0);

        /// <summary>
        /// List of possible 3D Types
        /// </summary>
        public enum AVStereo3DType
        {
            /// <summary>Video is not stereoscopic (and metadata has to be there).</summary>
            AV_STEREO3D_2D = 0,
            /// <summary>Views are next to each other.</summary>
            AV_STEREO3D_SIDEBYSIDE = 1,
            /// <summary>Views are on top of each other.</summary>
            AV_STEREO3D_TOPBOTTOM = 2,
            /// <summary>Views are alternated temporally.</summary>
            AV_STEREO3D_FRAMESEQUENCE = 3,
            /// <summary>Views are packed in a checkerboard-like structure per pixel.</summary>
            AV_STEREO3D_CHECKERBOARD = 4,
            /// <summary>Views are next to each other, but when upscaling apply a checkerboard pattern.</summary>
            AV_STEREO3D_SIDEBYSIDE_QUINCUNX = 5,
            /// <summary>Views are packed per line, as if interlaced.</summary>
            AV_STEREO3D_LINES = 6,
            /// <summary>Views are packed per column.</summary>
            AV_STEREO3D_COLUMNS = 7
        }

        /// <summary>
        /// Stereo 3D type: this structure describes how two videos are packed
        /// within a single video surface, with additional information as needed.
        /// </summary>
        /// <remark>
        /// The struct must be allocated with av_stereo3d_alloc() and
        /// its size is not a part of the public ABI.
        /// </remark>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct AVStereo3D
        {
            /// <summary>
            /// How views are packed within the video.
            /// </summary>
            public libavutil.AVStereo3DType type;

            /// <summary>
            /// Additional information about the frame packing.
            /// </summary>
            public int flags;
        }

        /// <summary>
        /// Allocate an AVStereo3D structure and set its fields to default values.
        /// The resulting struct can be freed using av_freep().
        /// </summary>
        /// <returns>
        /// An AVStereo3D filled with default values or NULL on failure.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_stereo3d_alloc")]
        public static extern libavutil.AVStereo3D* av_stereo3d_alloc();

        /// <summary>
        /// Allocate a complete AVFrameSideData and add it to the frame.
        /// </summary>
        /// <param name="frame">
        /// The frame which side data is added to.
        /// </param>
        /// <returns>
        /// The AVStereo3D structure to be filled by caller.
        /// </returns>
        [SuppressUnmanagedCodeSecurity]
        [DllImport(AVUTIL_DLL_NAME, CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, ExactSpelling = true,
            EntryPoint="av_stereo3d_create_side_data")]
        public static extern libavutil.AVStereo3D* av_stereo3d_create_side_data(libavutil.AVFrame* frame);
    }
}

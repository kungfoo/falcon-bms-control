using System;
using System.Runtime.InteropServices;

namespace F4Utils.Speech
{
    public class StreamTalk80
    {
        #region LH_ERRCODE enum

        public enum LH_ERRCODE
        {
            LH_SUCCESS = 0, /* everything is OK */
            LH_EFAILURE = -1, /* something went wrong */
            LH_EBADARG = -2, /* one of the given argument is incorrect */
            LH_BADHANDLE = -3 /* bad handle passed to function */
        }

        #endregion

        #region OPENCODERFLAGS enum

        /// <summary>
        ///   Possible flags for the dwFlags parameter of Open_Coder
        /// </summary>
        [Flags]
        public enum OPENCODERFLAGS
        {
            LINEAR_PCM_16_BIT = 0x0001,
            LINEAR_PCM_8_BIT = 0x0002,
            ADPCM_G721 = 0x0004,
            MULAW = 0x0008,
            ALAW = 0x0010
        }

        #endregion

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_Open_Coder", SetLastError = true)]
        public static extern IntPtr OpenCoder(OPENCODERFLAGS dwFlags);

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_Encode", SetLastError = true)]
        public static extern LH_ERRCODE Encode(
            IntPtr hAccess,
            IntPtr inputBufferPtr,
            [In] [Out] ref Int16 inputBufferLength,
            IntPtr outputBufferPtr,
            [In] [Out] ref Int16 outputBufferLength
            );

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_Close_Coder", SetLastError = true)]
        public static extern LH_ERRCODE CloseCoder(IntPtr hAccess);

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_Open_Decoder", SetLastError = true)]
        public static extern IntPtr OpenDecoder(OPENCODERFLAGS dwFlags);

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_Decode", SetLastError = true)]
        public static extern LH_ERRCODE Decode(
            IntPtr hAccess,
            IntPtr inputBufferPtr,
            [In] [Out] ref Int16 inputBufferLength,
            IntPtr outputBufferPtr,
            [In] [Out] ref Int16 outputBufferLength
            );

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_Close_Decoder", SetLastError = true)]
        public static extern LH_ERRCODE CloseDecoder(IntPtr hAccess);

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_GetCodecInfo", SetLastError = true)]
        public static extern void GetCodecInfo([In] [Out] ref CODECINFO lpCodecInfo);

        [DllImport("ST80W.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto,
            EntryPoint = "ST80_GetCodecInfoEx", SetLastError = true)]
        public static extern void GetCodecInfoEx([In] [Out] ref CODECINFOEX lpCodecInfoEx, Int32 cbSize);

        #region Nested type: CODECINFO

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CODECINFO
        {
            public Int16 wPCMBufferSize;
            public Int16 wCodedBufferSize;
            public Int16 wBitsPerSamplePCM;
            public Int32 dwSampleRate;
            public Int16 wFormatSubTag;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)] public string wFormatSubTagName;
            public Int32 dwDLLVersion;
        }

        #endregion

        #region Nested type: CODECINFOEX

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CODECINFOEX
        {
            public Int16 wFormatSubTag;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szFormatSubTagName;
            public bool bIsVariableBitRate;
            public bool bIsRealTimeEncoding;
            public bool bIsRealTimeDecoding;
            public bool bIsFloatingPoint;
            public Int16 wInputDataFormat;
            public Int32 dwInputSampleRate;
            public Int16 wInputBitsPerSample;
            public Int32 nAvgBytesPerSec;
            public Int16 wInputBufferSize;
            public Int16 wCodedBufferSize;
            public Int16 wMinimumCodedBufferSize;
            public Int32 dwDLLVersion;
            public Int16 cbSize; // size of extra data, not declared here.
        }

        #endregion
    }
}
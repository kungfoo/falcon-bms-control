using System;
using System.Runtime.InteropServices;
namespace F4SharedMem.Win32
{
    internal static class NativeMethods
    {
        public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const UInt32 SECTION_QUERY = 0x0001;
        public const UInt32 SECTION_MAP_WRITE = 0x0002;
        public const UInt32 SECTION_MAP_READ = 0x0004;
        public const UInt32 SECTION_MAP_EXECUTE = 0x0008;
        public const UInt32 SECTION_EXTEND_SIZE = 0x0010;
        public const UInt32 SECTION_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SECTION_QUERY |
            SECTION_MAP_WRITE |
            SECTION_MAP_READ |
            SECTION_MAP_EXECUTE |
            SECTION_EXTEND_SIZE);
        public const UInt32 FILE_MAP_ALL_ACCESS = SECTION_ALL_ACCESS;

        public const Int64 INVALID_HANDLE_VALUE = -1;

        [Flags]
        public enum PageProtection : uint
        {
            NoAccess = 0x01,
            Readonly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            Guard = 0x100,
            NoCache = 0x200,
            WriteCombine = 0x400,
        }
        [Flags]
        public enum SecurityAttributes : uint
        {
            Commit= 0x8000000,
            Image= 0x1000000,
            ImageNoExecute= 0x11000000,
            LargePages= 0x80000000,
            NoCache= 0x10000000,
            Reserve= 0x4000000,
            WriteCombine= 0x40000000,
        }
        [Flags]
        public enum AllocationType : uint
        {
            Commit= 0x00001000,
            Reserve= 0x00002000,
            Reset= 0x00080000,
            ResetUndo= 0x1000000,
            LargePages= 0x20000000,
            Physical= 0x00400000,
            TopDown= 0x00100000,
            WriteWatch= 0x00200000
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenFileMapping(uint dwDesiredAccess,
            bool bInheritHandle,
           string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint
           dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow,
           uint dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError =true)]
        public static extern int VirtualQuery(
            ref IntPtr lpAddress,
            ref MEMORY_BASIC_INFORMATION lpBuffer,
            IntPtr dwLength
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public UIntPtr BaseAddress;
            public UIntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }
        [DllImport("kernel32.dll", SetLastError =true)]
        public static extern bool GetFileSizeEx(IntPtr hFile, out IntPtr lpFileSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateFileMapping(IntPtr hFile,
                                                        IntPtr lpFileMappingAttributes, PageProtection flProtect,
                                                        uint dwMaximumSizeHigh,
                                                        uint dwMaximumSizeLow, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, PageProtection flProtect);
    }
}

using F4SharedMem.Headers;
using System;
using System.Runtime.InteropServices;

namespace F4SharedMem
{
    public sealed class Writer : IDisposable
    {
        private bool _disposed;
        private IntPtr _hPrimarySharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hSecondarySharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hOsbSharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hIntellivibeSharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hRadioClientControlSharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hRadioClientStatusSharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hStringSharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _hDrawingSharedMemoryAreaFileMappingObject = IntPtr.Zero;
        private IntPtr _lpPrimarySharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpSecondarySharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpOsbSharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpIntellivibeSharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpRadioClientControlSharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpRadioClientStatusSharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpStringSharedMemoryAreaBaseAddress = IntPtr.Zero;
        private IntPtr _lpDrawingSharedMemoryAreaBaseAddress = IntPtr.Zero;

        private const string _primarySharedMemoryAreaFileName = "FalconSharedMemoryArea";
        private const string _secondarySharedMemoryFileName = "FalconSharedMemoryArea2";
        private const string _osbSharedMemoryAreaFileName = "FalconSharedOsbMemoryArea";
        private const string _intellivibeSharedMemoryAreaFileName = "FalconIntellivibeSharedMemoryArea";
        private const string _radioClientControlSharedMemoryAreaFileName = "FalconRccSharedMemoryArea";
        private const string _radioClientStatusSharedMemoryAreaFileName = "FalconRcsSharedMemoryArea";
        private const string _stringSharedMemoryAreaFileName = "FalconSharedMemoryAreaString";
        private const string _drawingSharedMemoryAreaFileName = "FalconSharedMemoryAreaDrawing";

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public void WritePrimaryFlightData(byte[] data)
        {
            WriteSharedMemData(data, (uint)data.Length, _primarySharedMemoryAreaFileName, ref _hPrimarySharedMemoryAreaFileMappingObject, ref _lpPrimarySharedMemoryAreaBaseAddress);
        }

        public void WriteOSBData(byte[] data)
        {
            WriteSharedMemData(data, (uint)data.Length, _osbSharedMemoryAreaFileName, ref _hOsbSharedMemoryAreaFileMappingObject, ref _lpOsbSharedMemoryAreaBaseAddress);
        }

        public void WriteFlightData2(byte[] data)
        {
            WriteSharedMemData(data, (uint)data.Length, _secondarySharedMemoryFileName, ref _hSecondarySharedMemoryAreaFileMappingObject, ref _lpSecondarySharedMemoryAreaBaseAddress);
        }

        public void WriteIntellivibeData(byte[] data)
        {
            WriteSharedMemData(data, (uint)data.Length, _intellivibeSharedMemoryAreaFileName, ref _hIntellivibeSharedMemoryAreaFileMappingObject, ref _lpIntellivibeSharedMemoryAreaBaseAddress);
        }
        public void WriteRadioClientControlData(byte[] data)
        {
            WriteSharedMemData(data, (uint)data.Length, _radioClientControlSharedMemoryAreaFileName, ref _hRadioClientControlSharedMemoryAreaFileMappingObject, ref _lpRadioClientControlSharedMemoryAreaBaseAddress);
        }
        public void WriteRadioClientStatusData(byte[] data)
        {
            WriteSharedMemData(data, (uint)data.Length, _radioClientStatusSharedMemoryAreaFileName, ref _hRadioClientStatusSharedMemoryAreaFileMappingObject, ref _lpRadioClientStatusSharedMemoryAreaBaseAddress);
        }
        public void WriteStringData(byte[] data)
        {
            WriteSharedMemData(data, StringData.STRINGDATA_AREA_SIZE_MAX, _stringSharedMemoryAreaFileName, ref _hStringSharedMemoryAreaFileMappingObject, ref _lpStringSharedMemoryAreaBaseAddress);
        }
        public void WriteDrawingData(byte[] data)
        {
            WriteSharedMemData(data, DrawingData.DRAWINGDATA_AREA_SIZE_MAX, _drawingSharedMemoryAreaFileName, ref _hDrawingSharedMemoryAreaFileMappingObject, ref _lpDrawingSharedMemoryAreaBaseAddress);
        }
        private static void WriteSharedMemData(byte[] data, uint maxSize, string sharedMemoryAreaFileName, ref IntPtr hSharedMemoryFileMappingObject, ref IntPtr lpDestination)
        {
            if (data == null || data.Length == 0) return;
            if (hSharedMemoryFileMappingObject.Equals(IntPtr.Zero))
            {
                CreateSharedMemoryArea(sharedMemoryAreaFileName, maxSize, out hSharedMemoryFileMappingObject, out lpDestination);
            }
            if (hSharedMemoryFileMappingObject.Equals(IntPtr.Zero))
            {
                return;
            }
            if (!hSharedMemoryFileMappingObject.Equals(IntPtr.Zero))
            {
                F4SharedMem.Win32.NativeMethods.VirtualAlloc(lpDestination, (uint)data.Length, Win32.NativeMethods.AllocationType.Commit, Win32.NativeMethods.PageProtection.ReadWrite);
                Marshal.Copy(data, 0, lpDestination, (int)Math.Min(maxSize, data.Length));
            }
        }
        private static void CreateSharedMemoryArea(string sharedMemoryAreaFileName, uint length, out IntPtr hFileMappingObject, out IntPtr lpBaseAddress)
        {
            var createFileMappingFlags = (uint)F4SharedMem.Win32.NativeMethods.PageProtection.ReadWrite | (uint)Win32.NativeMethods.SecurityAttributes.Reserve;
            hFileMappingObject =
                F4SharedMem.Win32.NativeMethods.CreateFileMapping(new IntPtr(F4SharedMem.Win32.NativeMethods.INVALID_HANDLE_VALUE), IntPtr.Zero,
                                                (F4SharedMem.Win32.NativeMethods.PageProtection)createFileMappingFlags, 0, length,
                                                sharedMemoryAreaFileName);
            lpBaseAddress =
                F4SharedMem.Win32.NativeMethods.MapViewOfFile(hFileMappingObject,
                                            F4SharedMem.Win32.NativeMethods.SECTION_MAP_READ | F4SharedMem.Win32.NativeMethods.SECTION_MAP_WRITE, 0, 0, length);
        }


        private void CloseSharedMemFiles()
        {
            CloseSharedMemFile(ref _lpPrimarySharedMemoryAreaBaseAddress, ref _hPrimarySharedMemoryAreaFileMappingObject);
            CloseSharedMemFile(ref _lpSecondarySharedMemoryAreaBaseAddress, ref _hSecondarySharedMemoryAreaFileMappingObject);
            CloseSharedMemFile(ref _lpOsbSharedMemoryAreaBaseAddress, ref _hOsbSharedMemoryAreaFileMappingObject);
            CloseSharedMemFile(ref _lpIntellivibeSharedMemoryAreaBaseAddress, ref _hIntellivibeSharedMemoryAreaFileMappingObject);
            CloseSharedMemFile(ref _lpRadioClientControlSharedMemoryAreaBaseAddress, ref _hRadioClientControlSharedMemoryAreaFileMappingObject);
            CloseSharedMemFile(ref _lpRadioClientStatusSharedMemoryAreaBaseAddress, ref _hRadioClientStatusSharedMemoryAreaFileMappingObject);
        }
        private static void CloseSharedMemFile(ref IntPtr lpBaseAddress, ref IntPtr hFileMappingObject)
        {
            if (!hFileMappingObject.Equals(IntPtr.Zero))
            {
                F4SharedMem.Win32.NativeMethods.UnmapViewOfFile(lpBaseAddress);
                F4SharedMem.Win32.NativeMethods.CloseHandle(hFileMappingObject);
            }
            lpBaseAddress = IntPtr.Zero;
            hFileMappingObject = IntPtr.Zero;
        }

        internal void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    CloseSharedMemFiles();
                }

                _disposed = true;
            }
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using F4TexSharedMem.Win32;

namespace F4TexSharedMem
{
	[ComVisible(false)]
	public interface IReader
	{
		bool IsDataAvailable { get; }
		Bitmap FullImage { get; }
		void Dispose();
		IntPtr GetImagePointer(ref Rectangle rect);
		Bitmap GetImage(Rectangle rect);
	}

	[ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public sealed class Reader : IDisposable, IReader
	{
        private const string SharedMemoryFileName = "FalconTexturesSharedMemoryArea";
        private bool _dataAvailable;
        private PixelFormat _format = PixelFormat.Undefined;
        private bool _formatDetected;
        private IntPtr _hFileMappingObject = IntPtr.Zero;
        private IntPtr _lpStartAddress = IntPtr.Zero;
        private NativeMethods.DDSURFACEDESC2 _surfaceDesc;

        public bool IsDataAvailable
        {
            get
            {
                if (_hFileMappingObject.Equals(IntPtr.Zero))
                {
                    OpenSM();
                }
                if (_hFileMappingObject.Equals(IntPtr.Zero))
                {
                    _dataAvailable = false;
                }
                else if (_lpStartAddress.Equals(IntPtr.Zero))
                {
                    _dataAvailable = false;
                }
                else
                {
                    var checkFor = 0;
                    try
                    {
                        checkFor = Marshal.ReadInt32(_lpStartAddress);
                    }
                    catch
                    {
                    }
                    _dataAvailable = checkFor != 0;
                }
                return _dataAvailable;
            }
        }

        public Bitmap FullImage
        {
            get { return GetImage(Rectangle.Empty); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void DetectImageFormat()
        {
            _formatDetected = false;
            if (!_dataAvailable)
            {
                return;
            }
            _surfaceDesc =
                (NativeMethods.DDSURFACEDESC2)
                Marshal.PtrToStructure(new IntPtr(_lpStartAddress.ToInt64() + 4), typeof (NativeMethods.DDSURFACEDESC2));
            switch (_surfaceDesc.ddpfPixelFormat.dwRGBBitCount)
            {
                case 16:
                    _format = PixelFormat.Format16bppRgb555;
                    break;
                case 24:
                    _format = PixelFormat.Format24bppRgb;
                    break;
                case 32:
                    _format = PixelFormat.Format32bppRgb;
                    break;
            }
            if (_format != PixelFormat.Undefined)
            {
                _formatDetected = true; 
            }
            
        }

        public IntPtr GetImagePointer(ref Rectangle rect)
        {
            if (!CheckImage()) return IntPtr.Zero;
            var surfaceWidth = Math.Abs(_surfaceDesc.dwWidth);
            var surfaceHeight = Math.Abs(_surfaceDesc.dwHeight);

            if (rect == Rectangle.Empty) rect = new Rectangle(0, 0, surfaceWidth, surfaceHeight);
            rect = ClampRect(rect, surfaceWidth, surfaceHeight);
            if (rect == Rectangle.Empty) rect = new Rectangle(0, 0, surfaceWidth, surfaceHeight);

            var offset = (_surfaceDesc.lPitch*rect.Y) + (rect.X*(_surfaceDesc.ddpfPixelFormat.dwRGBBitCount/8));
            return new IntPtr(_lpStartAddress.ToInt64() + offset + 4 + _surfaceDesc.dwSize);
        }

        private static Rectangle ClampRect(Rectangle rect, int clampToWidth, int clampToHeight)
        {
            var toReturn = new Rectangle();
            var x = rect.X;
            var y = rect.Y;
            var width = rect.Width;
            var height = rect.Height;

            if (clampToWidth < 0) clampToWidth = 0;
            if (clampToHeight < 0) clampToHeight = 0;

            if (x < 0) x = 0;
            else if (x > clampToWidth) x = clampToWidth;
            if (y < 0) y = 0;
            else if (y > clampToHeight) y = clampToHeight;
            if (width < 0) width = 0;
            else if (width > clampToWidth) width = clampToWidth;
            if (height < 0) height = 0;
            else if (height > clampToHeight) height = clampToHeight;
            if (x + width > clampToWidth) width = clampToWidth - x;
            if (y + height > clampToHeight) height = clampToHeight - y;
            if (width < 0) width = 0;
            if (height < 0) height = 0;

            toReturn.X = x;
            toReturn.Y = y;
            toReturn.Width = width;
            toReturn.Height = height;

            return toReturn;
        }

        private bool CheckImage()
        {
            if (!_dataAvailable)
            {
                _dataAvailable = IsDataAvailable; //check again whether data is available
            }
            if (!_dataAvailable)
            {
                return false;
            }
            if (!_formatDetected)
            {
                DetectImageFormat();
            }
            return true;
        }

        public Bitmap GetImage(Rectangle rect)
        {
            if (!CheckImage()) return null;
            var start = GetImagePointer(ref rect);
            var bmp = new Bitmap(rect.Width, rect.Height, _surfaceDesc.lPitch, _format, start);
            return bmp;
        }

        private void OpenSM()
        {
            CloseSM();
            _hFileMappingObject = NativeMethods.OpenFileMapping(NativeMethods.SECTION_MAP_READ, false,
                                                                SharedMemoryFileName);
            _lpStartAddress = NativeMethods.MapViewOfFile(_hFileMappingObject, NativeMethods.SECTION_MAP_READ, 0, 0,
                                                          IntPtr.Zero);
        }

        private void CloseSM()
        {
	        if (_hFileMappingObject.Equals(IntPtr.Zero)) return;
	        NativeMethods.UnmapViewOfFile(_lpStartAddress);
	        NativeMethods.CloseHandle(_hFileMappingObject);
            _dataAvailable=false;
            _format=PixelFormat.Undefined;
            _formatDetected=false;
            _hFileMappingObject=IntPtr.Zero;
            _lpStartAddress=IntPtr.Zero;
            _surfaceDesc=new NativeMethods.DDSURFACEDESC2();
        }

        internal void Dispose(bool disposing)
        {
		    CloseSM();
        }
    }
}
using System;
using System.Collections.Generic;
using log4net;
using Microsoft.VisualBasic.Devices;

namespace Common.InputSupport.Phcc
{
    public class PHCCDeviceManager : IDisposable
    {
        private const int MinAnalogDataSourceVal = 1;
        private const int MaxAnalogDataSourceVal = 32767;
        private static PHCCDeviceManager _instance;
        private static readonly ILog _log = LogManager.GetLogger(typeof(PHCCDeviceManager));
        private bool _isDisposed;

        private PHCCDeviceManager()
        {
        }

        /// <summary>
        ///     Public implementation of IDisposable.Dispose().  Cleans up managed
        ///     and unmanaged resources used by this object before allowing garbage collection
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Standard finalizer, which will call Dispose() if this object is not
        ///     manually disposed.  Ordinarily called only by the garbage collector.
        /// </summary>
        ~PHCCDeviceManager()
        {
            Dispose();
        }

        public static PHCCPhysicalControlInfo[] GetControlsOnDevice(PHCCPhysicalDeviceInfo device)
        {
            return GetControlsOnDevice(device, true);
        }

        public static PHCCPhysicalControlInfo[] GetControlsOnDevice(PHCCPhysicalDeviceInfo device, bool throwOnFail)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }
            var controls = new List<PHCCPhysicalControlInfo>();
            for (var i = 0; i < 1024; i++)
            {
                var thisControl = new PHCCPhysicalControlInfo(device, i, ControlType.Button, "Button " + (i + 1));
                controls.Add(thisControl);
            }
            for (var i = 0; i < 35; i++)
            {
                var thisControl = new PHCCPhysicalControlInfo(device, i, ControlType.Axis, "Axis " + (i + 1));
                controls.Add(thisControl);
            }

            var toReturn = controls.ToArray();
            return toReturn;
        }

        public static PHCCPhysicalDeviceInfo[] GetDevices()
        {
            return GetDevices(true);
        }

        public static PHCCPhysicalDeviceInfo[] GetDevices(bool throwOnFail)
        {
            var devices = new List<PHCCPhysicalDeviceInfo>();
            var ports = new Ports();

            foreach (var portName in ports.SerialPortNames)
            {
                var deviceInfo = new PHCCPhysicalDeviceInfo(portName, "PHCC device on " + portName);
                try
                {
                    if (
                        PHCCDeviceMonitor.GetInstance(deviceInfo, MinAnalogDataSourceVal,
                                MaxAnalogDataSourceVal)
                            .IsDeviceAttached(false))
                    {
                        devices.Add(deviceInfo);
                    }
                }
                catch (Exception ex)
                {
                    _log.Debug(ex.Message, ex);
                }
            }
            var toReturn = devices.ToArray();
            return toReturn;
        }

        public static PHCCDeviceManager GetInstance()
        {
            return _instance ?? (_instance = new PHCCDeviceManager());
        }

        public static bool IsDeviceAttached(PHCCPhysicalDeviceInfo device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }
            return
                PHCCDeviceMonitor.GetInstance(device, MinAnalogDataSourceVal,
                        MaxAnalogDataSourceVal)
                    .IsDeviceAttached(true);
        }

        public static bool IsDeviceAttached(PHCCPhysicalDeviceInfo device, bool throwOnFail)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }
            return
                PHCCDeviceMonitor.GetInstance(device, MinAnalogDataSourceVal,
                        MaxAnalogDataSourceVal)
                    .IsDeviceAttached(throwOnFail);
        }

        /// <summary>
        ///     Private implementation of Dispose()
        /// </summary>
        /// <param name="disposing">
        ///     flag to indicate if we should actually perform disposal.  Distinguishes the private method
        ///     signature from the public signature.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _instance = null;
                }
            }
            _isDisposed = true;
        }
    }
}
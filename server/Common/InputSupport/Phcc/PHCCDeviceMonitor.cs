using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using log4net;
using Phcc;

namespace Common.InputSupport.Phcc
{
    public class PHCCStateChangedEventArgs : EventArgs
    {
        public PHCCStateChangedEventArgs()
        {
        }

        public PHCCStateChangedEventArgs(PHCCInputState? newState, PHCCInputState? previousState,
            ControlType controlType, int controlIndex)
        {
            NewState = newState;
            PreviousState = previousState;
            ControlType = controlType;
            ControlIndex = controlIndex;
        }

        public int? ControlIndex { get; }

        public ControlType? ControlType { get; }

        public PHCCInputState? NewState { get; }

        public PHCCInputState? PreviousState { get; }
    }

    public sealed class PHCCDeviceMonitor : DeviceMonitor
    {
        public delegate void PHCCStateChangedEventHandler(object sender, PHCCStateChangedEventArgs e);

        private static readonly ILog _log = LogManager.GetLogger(typeof(PHCCDeviceMonitor));

        /// <summary>
        ///     Class variable to hold all references to all instantiated device monitors of this type
        /// </summary>
        private static readonly Dictionary<PHCCPhysicalDeviceInfo, PHCCDeviceMonitor> _monitors =
            new Dictionary<PHCCPhysicalDeviceInfo, PHCCDeviceMonitor>();

        private readonly int _axisRangeMax;
        private readonly int _axisRangeMin;

        private readonly object _stateLock = new object();
        private Device _deviceInterface;
        private PHCCInputState? _state;

        /// <summary>
        ///     Hidden constructor -- forces callers to use one of the static factory methods
        ///     on this class.
        ///     Creates a new PHCCDeviceMonitor object
        ///     <param name="deviceInfo">a PHCCPhysicalDeviceInfo object representing the device to monitor.</param>
        ///     <param name="axisRangeMin">
        ///         an integer specifying the value that should be reported when an axis is at its minimum
        ///         value
        ///     </param>
        ///     <param name="axisRangeMax">
        ///         an integer specifying the value that should be reported when an axis is at its maximum
        ///         value
        ///     </param>
        /// </summary>
        private PHCCDeviceMonitor(PHCCPhysicalDeviceInfo deviceInfo, int axisRangeMin, int axisRangeMax)
        {
            DeviceInfo = deviceInfo;
            _axisRangeMin = axisRangeMin;
            _axisRangeMax = axisRangeMax;
            Prepare();
        }

        /// <summary>
        ///     Returns a <see cref="PHCCInputState" /> object containing the most-recently-polled input state of the device being
        ///     monitored by this object
        /// </summary>
        public PHCCInputState? CurrentState
        {
            get
            {
                lock (_stateLock)
                {
                    return _state;
                }
            }
        }

        public PHCCPhysicalDeviceInfo DeviceInfo { get; }

        /// <summary>
        ///     Returns a <see cref="PHCCInputState" /> object containing the previous input state of the device being monitored by
        ///     this object
        /// </summary>
        public PHCCInputState? PreviousState { get; private set; }

        /// <summary>
        ///     Standard finalizer, which will call Dispose() if this object is not
        ///     manually disposed.  Ordinarily called only by the garbage collector.
        /// </summary>
        ~PHCCDeviceMonitor()
        {
            Dispose();
        }

        public event PHCCStateChangedEventHandler StateChanged;

        /// <summary>
        ///     Public implementation of IDisposable.Dispose().  Cleans up managed
        ///     and unmanaged resources used by this object before allowing garbage collection
        /// </summary>
        public new void Dispose()
        {
            if (_deviceInterface != null)
            {
                try
                {
                    if (_deviceInterface.SerialPort?.BaseStream != null)
                    {
                        GC.ReRegisterForFinalize(_deviceInterface.SerialPort.BaseStream);
                    }
                    _deviceInterface.Dispose();
                }
                catch (Exception e)
                {
                    _log.Debug(e.Message, e);
                }
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Compares this object to another one to determine if they are equal.  Equality for this type of object simply means
        ///     that the other object must be of the same type and must be monitoring the same DirectInput device.
        /// </summary>
        /// <param name="obj">An object to compare this object to</param>
        /// <returns>
        ///     a boolean, set to true, if the this object is equal to the specified object, and set to false, if they are not
        ///     equal.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            // safe because of the GetType check
            var js = (PHCCDeviceMonitor) obj;

            // use this pattern to compare value members
            if (!DeviceInfo.Equals(js.DeviceInfo))
            {
                return false;
            }

            return true;
        }

        public PHCCInputState? GetCurrentInputState(bool throwOnFail)
        {
            try
            {
                if (!_prepared && !_preparing)
                {
                    Prepare();
                }

                PHCCInputState? newState = null;
                lock (_stateLock)
                {
                    try
                    {
                        if (_deviceInterface != null)
                        {
                            var rawAnalogInputs = _deviceInterface.AnalogInputs;
                            var rawDigitalInputs = _deviceInterface.DigitalInputs;
                            var thisState = new PHCCInputState
                            {
                                AnalogInputs = new short[35],
                                DigitalInputs = new bool[1024]
                            };
                            for (var i = 0; i < rawAnalogInputs.Length; i++)
                                thisState.AnalogInputs[i] = ScaleRawValue(rawAnalogInputs[i]);
                            thisState.DigitalInputs = rawDigitalInputs;
                            newState = thisState;
                        }
                    }
                    catch (ApplicationException e)
                    {
                        _log.Debug(e.Message, e);
                        if (throwOnFail) throw;
                    }
                    if (newState.HasValue)
                    {
                        PreviousState = _state;
                        _state = newState;
                    }
                }
                return newState;
            }
            catch (ApplicationException e)
            {
                _log.Debug(e.Message, e);
                _prepared = false;
                if (throwOnFail)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        ///     Gets an integer "hash" representation of this object, for use in hashtables.
        /// </summary>
        /// <returns>
        ///     an integer containing a numeric hash of this object's variables.  When two objects are Equal, their hashes
        ///     should be equal as well.
        /// </returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        ///     Factory method to create instances of this class.  Stands in place of a constructor,
        ///     in order to re-use instances
        ///     when relevant constructor parameters are the same
        /// </summary>
        /// <param name="deviceInfo">a PHCCPhysicalDeviceInfo object representing the PHCC device to monitor</param>
        /// <param name="axisRangeMin">an integer specifying the value that should be reported when an axis is at its minimum value</param>
        /// <param name="axisRangeMax">an integer specifying the value that should be reported when an axis is at its maximum value</param>
        /// <returns>
        ///     a PHCCDeviceMonitor object representing the PHCC device
        ///     being monitored, either created newly from-scratch, or returned from
        ///     this class's internal object pool if a monitor instance already exists
        /// </returns>
        public static PHCCDeviceMonitor GetInstance(PHCCPhysicalDeviceInfo deviceInfo, int axisRangeMin,
            int axisRangeMax)
        {
            PHCCDeviceMonitor monitor = null;
            if (_monitors.ContainsKey(deviceInfo))
            {
                return _monitors[deviceInfo];
            }
            try
            {
                monitor = new PHCCDeviceMonitor(deviceInfo, axisRangeMin, axisRangeMax);
                _monitors.Add(deviceInfo, monitor);
            }
            catch (TimeoutException e)
            {
                _log.Debug(e.Message, e);
            }
            catch (IOException e)
            {
                _log.Debug(e.Message, e);
            }
            return monitor;
        }

        public bool IsDeviceAttached(bool throwOnFail)
        {
            if (!_prepared && !_preparing)
            {
                Prepare();
            }
            string firmwareVersion = null;
            if (_deviceInterface != null)
            {
                try
                {
                    firmwareVersion = _deviceInterface.FirmwareVersion;
                    _log.DebugFormat("PHCC firmware version: {0}", firmwareVersion);
                }
                catch (InvalidOperationException e)
                {
                    _log.Debug(e.Message, e);
                    if (throwOnFail) throw;
                }
                catch (TimeoutException e2)
                {
                    _log.Debug(e2.Message, e2);
                    if (throwOnFail) throw;
                }
            }
            return  firmwareVersion != null && firmwareVersion.ToLowerInvariant().Contains("phcc");
        }

        public PHCCInputState? Poll()
        {
            return GetCurrentInputState(true);
        }

        /// <summary>
        ///     Gets a string representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return GetType().Name + ":DeviceInfo=" + DeviceInfo;
        }

        /// <summary>
        ///     Initializes this object's state and sets up PHCC Device objects
        ///     to monitor the PHCC device instance that this object
        ///     is responsible for.
        ///     During preparation, the _preparing flag is raised.  Subsequent concurrent calls to
        ///     Prepare() will simply wait until the _preparing flag is lowered.
        /// </summary>
        protected override void Prepare()
        {
            var elapsed = 0;
            const int timeout = 500;

            while (_preparing && elapsed <= timeout)
            {
                Thread.Sleep(20);
                elapsed += 20;
            }
            if (!_preparing)
            {
                try
                {
                    try
                    {
                        _preparing = true;
                        //close any existing connection to the device
                        if (_deviceInterface != null)
                        {
                            try
                            {
                                _deviceInterface.Dispose();
                                _deviceInterface = null;
                            }
                            catch (Exception e)
                            {
                                _log.Debug(e.Message, e);
                            }
                        }
                        try
                        {
                            //create a new PHCC Interface to use for talking to the device
                            _deviceInterface = new Device(DeviceInfo.Key.ToString());
                            GC.SuppressFinalize(_deviceInterface.SerialPort.BaseStream);
                        }
                        catch (IOException ex)
                        {
                            _log.Debug(ex.Message, ex);
                        }
                        //check if device is attached
                        var attached = IsDeviceAttached(false);
                        if (!attached)
                        {
                            _preparing = false;
                            _prepared = false;
                            try
                            {
                                if (_deviceInterface != null)
                                {
                                    _deviceInterface.Dispose();
                                    _deviceInterface = null;
                                }
                            }
                            catch (Exception e)
                            {
                                _log.Debug(e.Message, e);
                            }
                            return;
                        }
                        //_deviceInterface.Reset();
                        GetCurrentInputState(false);
                        if (_deviceInterface != null)
                        {
                            _deviceInterface.StartTalking();
                            _deviceInterface.AnalogInputChanged += DeviceInterfaceAnalogInputChanged;
                            _deviceInterface.DigitalInputChanged += DeviceInterfaceDigitalInputChanged;
                        }
                    }
                    catch (ApplicationException)
                    {
                        _preparing = false;
                        _prepared = false;
                        if (true) throw;
                    }

                    _prepared = true;
                }
                catch (ApplicationException e)
                {
                    _log.Debug(e.Message, e);
                    _prepared = false;
                    if (true) throw;
                }
                finally
                {
                    _preparing = false;
                }
            }
        }

        private void DeviceInterfaceAnalogInputChanged(object sender, AnalogInputChangedEventArgs e)
        {
            lock (_stateLock)
            {
                PreviousState = _state;
                if (PreviousState.HasValue)
                {
                    _state = (PHCCInputState) ((ICloneable) PreviousState).Clone();
                }
                var thisState = new PHCCInputState();
                if (_state.HasValue)
                {
                    thisState = _state.Value;
                }
                else
                {
                    thisState.AnalogInputs = new short[35];
                    thisState.DigitalInputs = new bool[1024];
                }
                thisState.AnalogInputs[e.Index] = ScaleRawValue(e.NewValue);
                _state = thisState;

                StateChanged?.Invoke(this, new PHCCStateChangedEventArgs(_state, PreviousState, ControlType.Axis, e.Index));
            }
        }

        private void DeviceInterfaceDigitalInputChanged(object sender, DigitalInputChangedEventArgs e)
        {
            lock (_stateLock)
            {
                PreviousState = _state;
                if (PreviousState.HasValue)
                {
                    _state = (PHCCInputState) ((ICloneable) PreviousState).Clone();
                }
                var thisState = new PHCCInputState();
                if (_state.HasValue)
                {
                    thisState = _state.Value;
                }
                else
                {
                    thisState.AnalogInputs = new short[35];
                    thisState.DigitalInputs = new bool[1024];
                }
                thisState.DigitalInputs[e.Index] = e.NewValue;
                _state = thisState;

                StateChanged?.Invoke(this,
                    new PHCCStateChangedEventArgs(_state, PreviousState, ControlType.Button, e.Index));
            }
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
                    //dispose of managed resources
                }
            }
            // Code to dispose the un-managed resources of the class
            _isDisposed = true;
        }

        private short ScaleRawValue(short rawValue)
        {
            long outputRange = Math.Abs(_axisRangeMax - _axisRangeMin); //size of output range
            const long maxRawValue = 1023;
            var pct = rawValue / (double) maxRawValue;
            //percentage-wise, how large is our raw value compared to the maximum raw value?
            var convertedVal = (int) Math.Round(outputRange * pct, 0) + _axisRangeMin;
            return (short) convertedVal;
        }
    }
}
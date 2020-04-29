using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using log4net;
using SlimDX.DirectInput;
namespace Common.InputSupport.DirectInput
{
    public sealed class DIDeviceMonitor : DeviceMonitor, IDisposable
    {
        public delegate void DIStateChangedEventHandler(object sender, DIStateChangedEventArgs e);

        /// <summary>
        ///     Class variable to hold all references to all instantiated device monitors of this type
        /// </summary>
        private static readonly Dictionary<Guid, DIDeviceMonitor> _monitors = new Dictionary<Guid, DIDeviceMonitor>();

        private static readonly ILog Log = LogManager.GetLogger(typeof(DIDeviceMonitor));

        /// <summary>
        ///     Value passed in this object's constructor, indicating the integer value
        ///     that should represent an axis' maximum reported value for any axis being
        ///     monitored by this object instance.  This enables DirectInput to perform
        ///     translation between the internal values used by the device, and the values
        ///     that are expected by calling code.
        /// </summary>
        private readonly int _axisRangeMax = 1024;

        /// <summary>
        ///     Value passed in this object's constructor, indicating the integer value
        ///     that should represent an axis' minimum reported value for any axis being
        ///     monitored by this object instance.  This enables DirectInput to perform
        ///     translation between the internal values used by the device, and the values
        ///     that are expected by calling code.
        /// </summary>
        private readonly int _axisRangeMin;

        private readonly object _stateLock = new object();
        private Thread _eventMonitorThread;

        /// <summary>
        ///     The previous JoystickState structure from the DirectInput Device
        ///     being monitored by this object instance
        /// </summary>
        private JoystickState _prevState;

        /// <summary>
        ///     The last-polled JoystickState structure from the DirectInput Device
        ///     being monitored by this object instance
        /// </summary>
        private JoystickState _state;

        /// <summary>
        ///     The DirectInput Device object being monitored by this object instance
        /// </summary>
        private Joystick _underlyingDxDevice;
        private SlimDX.DirectInput.DirectInput _directInput = new SlimDX.DirectInput.DirectInput();
        private Control _parentForm;

        /// <summary>
        ///     Hidden default constructor -- forces callers to use one of the static
        ///     factory methods on this class
        /// </summary>
        private DIDeviceMonitor()
        {
        }

        /// <summary>
        ///     Hidden constructor -- forces callers to use one of the static factory methods
        ///     on this class.
        ///     Creates a new DIDeviceMonitor object
        /// </summary>
        /// <param name="device">
        ///     a DIPhysicalDeviceInfo object representing the
        ///     DirectInput Device Instance to monitor
        /// </param>
        /// <param name="parentForm">
        ///     and an (optional) reference to a parent Windows Form
        ///     which will receive events directly from DirectInput if eventing is enabled
        ///     (currently, not implemented)
        /// </param>
        /// <param name="axisRangeMin">
        ///     Value to report when an axis is reporting its
        ///     MINIMUM value
        /// </param>
        /// <param name="axisRangeMax">
        ///     Value to report when an axis is reporting its
        ///     MAXIMUM value
        /// </param>
        private DIDeviceMonitor(DIPhysicalDeviceInfo device, Control parentForm, int axisRangeMin, int axisRangeMax)
        {
            DeviceInfo = device;
            _parentForm = parentForm;
            _axisRangeMin = axisRangeMin;
            _axisRangeMax = axisRangeMax;
            Prepare();
        }

        public int AxisRangeMax => _axisRangeMax;
        public int AxisRangeMin => _axisRangeMin;

        /// <summary>
        ///     Returns a DirectInput JoystickState structure representing the most-recently-polled joystick state of the device
        ///     being monitored by this object
        /// </summary>
        public JoystickState CurrentState
        {
            get
            {
                lock (_stateLock)
                {
                    return _state;
                }
            }
        }

        public DIPhysicalDeviceInfo DeviceInfo { get; }

        /// <summary>
        ///     Returns a DirectInput JoystickState structure representing the previous joystick state of the device being
        ///     monitored by this object
        /// </summary>
        public JoystickState PreviousState
        {
            get
            {
                lock (_stateLock)
                {
                    return _prevState;
                }
            }
        }

        /// <summary>
        ///     Returns the DirectInput Device object being monitored by this object
        /// </summary>
        public Device UnderlyingDirectXDevice => _underlyingDxDevice;

        /// <summary>
        ///     Returns an int? containing the Vendor Identity and the Product Id for the
        ///     DirectInput device being monitored by this object.  This token can
        ///     be examined to determine the type of device being monitored, and can be
        ///     used to distinguish physical devices from virtual devices, or to distinguish
        ///     among various manufacturers and even specific products.  The high 16 bits
        ///     contain the Vendor Identity and the low 16 bits contain the Product ID.
        ///     If this token cannot be obtained, then this method will return an int?
        ///     with no value (.hasValue == false).
        /// </summary>
        public int? VendorIdentityProductId
        {
            get
            {
                if (!Prepared)
                {
                    Prepare();
                }
                if (_underlyingDxDevice != null)
                {
                    return (int)((long)(_underlyingDxDevice.Properties.VendorId << 16) | (long)_underlyingDxDevice.Properties.ProductId);
                }
                return null;
            }
        }

        /// <summary>
        ///     Public implementation of IDisposable.Dispose().  Cleans up managed
        ///     and unmanaged resources used by this object before allowing garbage collection
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Standard finalizer, which will call Dispose() if this object is not
        ///     manually disposed.  Ordinarily called only by the garbage collector.
        /// </summary>
        ~DIDeviceMonitor()
        {
            Dispose();
        }

        public event DIStateChangedEventHandler StateChanged;

        public void DIEventMonitorThreadWork()
        {
            if (_underlyingDxDevice != null)
            {
                try
                {
                    GetNewJoyState();
                    while (!_isDisposed)
                    {
                        try {
                            Thread.Sleep(50);
                            GetNewJoyState();
                        }
                        catch (TimeoutException)
                        {
                            Application.DoEvents();
                            continue;
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                catch (ThreadInterruptedException) {}
            }
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
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            // safe because of the GetType check
            var js = (DIDeviceMonitor) obj;

            // use this pattern to compare value members
            if (!DeviceInfo.Guid.Equals(js.DeviceInfo.Guid)) return false;

            return true;
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
        /// <param name="device">
        ///     a <see cfef="DIPhysicalDeviceInfo" /> object representing the
        ///     DirectInput Device Instance to monitor
        /// </param>
        /// <param name="parentForm">
        ///     and an (optional) reference to a parent Windows Form
        ///     which will receive events directly from DirectInput if eventing is enabled
        ///     (currently, not implemented)
        /// </param>
        /// <param name="axisRangeMin">
        ///     Value to report when an axis is reporting its
        ///     MINIMUM value
        /// </param>
        /// <param name="axisRangeMax">
        ///     Value to report when an axis is reporting its
        ///     MAXIMUM value
        /// </param>
        /// <returns>
        ///     a DIDeviceMonitor object representing the DirectInput device being
        ///     monitored, either created newly from-scratch, or returned from this class's
        ///     internal object pool if a monitor instance already exists
        /// </returns>
        public static DIDeviceMonitor GetInstance(DIPhysicalDeviceInfo device, Control parentForm, int axisRangeMin,
            int axisRangeMax)
        {
            var deviceId = new Guid(device.Key.ToString());
            if (_monitors.ContainsKey(deviceId))
            {
                return _monitors[deviceId];
            }
            var monitor = new DIDeviceMonitor(device, parentForm, axisRangeMin, axisRangeMax);
            _monitors.Add(deviceId, monitor);
            return monitor;
        }

        /// <summary>
        ///     Polls the monitored DirectInput device.  This method also updates the
        ///     previous state variable to the results of the previous polling,
        ///     so that the current and previous states can be compared to determine what has changed
        ///     in the latest poll.
        /// </summary>
        /// <returns>
        ///     and returns a nullable JoystickState structure representing
        ///     the current joystick state discovered during polling
        /// </returns>
        public JoystickState Poll()
        {
            try
            {
                if (!Prepared)
                {
                    Prepare();
                }
                if (_underlyingDxDevice != null)
                {
                    _underlyingDxDevice.Poll();
                    lock (_stateLock)
                    {
                        _prevState = _state;
                        _state = _underlyingDxDevice.GetCurrentState();
                    }
                }
                else
                {
                    Prepared = false;
                }
            }
            catch
            { 
                Prepared = false;
            }
            return _state;
        }

        /// <summary>
        ///     Gets a string representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return GetType().Name + ":" + DeviceInfo.Guid;
        }

        /// <summary>
        ///     Initializes this object's state and sets up a DirectInput Device object
        ///     to monitor the device instance specified in this object's _guid variable.
        ///     During preparation, the _preparing flag is raised.
        /// </summary>
        protected override void Prepare()
        {
            if (!Preparing)
            {
                try
                {
                    Common.Util.DisposeObject(_underlyingDxDevice);
                    Preparing = true;
                    _underlyingDxDevice = new Joystick(_directInput,DeviceInfo.Guid);
                    if (_underlyingDxDevice != null)
                    {
                        _underlyingDxDevice.SetCooperativeLevel(_parentForm, 
                            CooperativeLevel.Nonexclusive | CooperativeLevel.Background);

                        //Set joystick axis ranges.
                        _underlyingDxDevice.Properties.SetRange(_axisRangeMin, _axisRangeMax);
                        _underlyingDxDevice.Properties.AxisMode = DeviceAxisMode.Absolute;
                        _underlyingDxDevice.Acquire();
                        _eventMonitorThread = new Thread(DIEventMonitorThreadWork);
                        _eventMonitorThread.SetApartmentState(ApartmentState.STA);
                        _eventMonitorThread.Name = "DIMonitorThread:" + _underlyingDxDevice.Information.InstanceGuid;
                        _eventMonitorThread.Priority = ThreadPriority.Normal;
                        _eventMonitorThread.IsBackground = true;
                        _eventMonitorThread.Start();

                        Prepared = true;
                    }
                }
                catch 
                {
                    Prepared = false;
                }
                finally
                {
                    Preparing = false;
                }
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
            if (_eventMonitorThread != null)
            {
                _eventMonitorThread.Interrupt();
                _eventMonitorThread.Abort();
            }
            if (!IsDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        if (_monitors != null && DeviceInfo != null && _monitors.ContainsKey(DeviceInfo.Guid))
                        {
                            _monitors.Remove(DeviceInfo.Guid);
                        }
                    }
                    catch { }
                    if (_underlyingDxDevice != null)
                    {
                        try
                        {
                            _underlyingDxDevice.Unacquire();
                        }
                        catch { }

                        try
                        {
                            Common.Util.DisposeObject(_underlyingDxDevice);
                        }
                        catch { }
                    }
                    if (_directInput !=null)
                    {
                        try
                        {
                            Common.Util.DisposeObject(_directInput);
                        }
                        catch { }
                    }
                }
            }
            // Code to dispose the un-managed resources of the class
            IsDisposed = true;
        }

        private void GetNewJoyState()
        {
            lock (_stateLock)
            {
                _prevState = _state;
                try
                {
                    if (_underlyingDxDevice != null)
                    {
                        _state = _underlyingDxDevice.GetCurrentState();
                        StateChanged?.Invoke(this, new DIStateChangedEventArgs(_prevState, _state));
                    }
                }
                catch 
                {
                    Dispose();
                    Prepared = false;
                }
            }
        }
    }
}
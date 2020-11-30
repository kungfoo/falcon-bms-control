using System;
using System.Collections.Generic;
using System.Threading;
using log4net;

namespace Common.InputSupport.BetaInnovations
{
    public sealed class BIDeviceMonitor : DeviceMonitor
    {
        /// <summary>
        ///     Class variable to hold all references to all instantiated device monitors of this type
        /// </summary>
        private static readonly ILog _log = LogManager.GetLogger(typeof(BIDeviceMonitor));

        private static readonly Dictionary<BIPhysicalDeviceInfo, BIDeviceMonitor> _monitors =
            new Dictionary<BIPhysicalDeviceInfo, BIDeviceMonitor>();

        private BIDeviceManager _manager;

        /// <summary>
        ///     Hidden constructor -- forces callers to use one of the static factory methods
        ///     on this class.
        ///     Creates a new BIDeviceMonitor object
        ///     <param name="device">a BIPhysicalDeviceInfo object representing the device to monitor.</param>
        /// </summary>
        private BIDeviceMonitor(BIPhysicalDeviceInfo device)
        {
            Device = device;
            Prepare();
        }

        /// <summary>
        ///     Returns an array representing the most-recently-polled input state of the device being monitored by this object
        /// </summary>
        public bool[] CurrentState { get; private set; }

        public BIPhysicalDeviceInfo Device { get; }

        /// <summary>
        ///     Returns an array representing the previous input state of the device being monitored by this object
        /// </summary>
        public bool[] PreviousState { get; private set; }

        /// <summary>
        ///     Standard finalizer, which will call Dispose() if this object is not
        ///     manually disposed.  Ordinarily called only by the garbage collector.
        /// </summary>
        ~BIDeviceMonitor()
        {
            Dispose();
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
            var js = (BIDeviceMonitor) obj;

            // use this pattern to compare value members
            return Device.Equals(js.Device);
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
        /// <param name="device">a BIPhysicalDeviceInfo object representing the Beta Innovations device to monitor</param>
        /// <returns>
        ///     a BIDeviceMonitor object representing the BetaInnovations device
        ///     being monitored, either created newly from-scratch, or returned from
        ///     this class's internal object pool if a monitor instance already exists
        /// </returns>
        public static BIDeviceMonitor GetInstance(BIPhysicalDeviceInfo device)
        {
            if (_monitors.ContainsKey(device))
            {
                return _monitors[device];
            }

            var monitor = new BIDeviceMonitor(device);
            _monitors.Add(device, monitor);
            return monitor;
        }

        public bool[] Poll()
        {
            return Poll(true);
        }

        public bool[] Poll(bool throwOnFail)
        {
            try
            {
                if (!_prepared)
                {
                    Prepare();
                }
                //if (!_manager.IsDeviceAttached(_device, throwOnFail))
                //{
                //    _prepared = false;
                //}
                //else
                //{
                var newState = _manager.Poll(Device, throwOnFail);
                if (newState == null) return null;
                PreviousState = CurrentState;
                CurrentState = newState;
                return newState;
                //}
            }
            catch (BIException e)
            {
                _log.Error(e.Message, e);
                _prepared = false;
                if (throwOnFail)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        ///     Gets a string representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return GetType().Name + ":Device=" + Device;
        }

        /// <summary>
        ///     Initializes this object's state and sets up BetaInnovations SDK objects
        ///     to monitor the BetaInnoviations device instance that this object
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
                    _manager = BIDeviceManager.GetInstance();
                    try
                    {
                        _preparing = true;
                        //check if device is attached
                        var connected = _manager.IsDeviceAttached(Device.Key.ToString(), false);
                        if (!connected)
                        {
                            _preparing = false;
                            _prepared = false;
                            return;
                        }
                    }
                    catch (BIException e)
                    {
                        _log.Error(e.Message, e);
                        _preparing = false;
                        _prepared = false;
                        return;
                    }

                    _prepared = true;
                }
                catch (BIException ex)
                {
                    _log.Error(ex.Message, ex);
                    _prepared = false;
                    throw;
                }
                finally
                {
                    _preparing = false;
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
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_manager != null)
                    {
                        try
                        {
                            _manager.Dispose();
                        }
                        catch (ApplicationException e)
                        {
                            _log.Debug(e.Message, e);
                        }
                    }
                }
            }
            // Code to dispose the un-managed resources of the class
            _isDisposed = true;
        }
    }
}
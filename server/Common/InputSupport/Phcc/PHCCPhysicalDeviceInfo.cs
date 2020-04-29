using System;

namespace Common.InputSupport.Phcc
{
    /// <summary>
    ///     Represents a specific physical PHCC device
    /// </summary>
    [Serializable]
    public class PHCCPhysicalDeviceInfo : PhysicalDeviceInfo
    {
        /// <summary>
        /// </summary>
        public PHCCPhysicalDeviceInfo()
        {
        }

        /// <summary>
        ///     Constructs a PHCCPhysicalDeviceInfo, given a COM Port name
        ///     and an (optional) alias ("Friendly name")
        ///     to use for the device
        /// </summary>
        /// <param name="portName">
        ///     the COM port name (i.e "COM1", "COM2", etc.)
        ///     of the PHCC device to be represented by the newly-created object
        /// </param>
        /// <param name="alias">
        ///     a string containing a "friendly name" (alias)
        ///     to associate with the device being represented
        /// </param>
        public PHCCPhysicalDeviceInfo(string portName, string alias)
            : base(portName, alias)
        {
        }

        /// <summary>
        ///     Discovers the physical controls that appear on this device,
        ///     as reported by the PHCC Interface Library, and stores them as an array
        ///     of PhysicalControlInfo objects at the instance level.
        ///     NOT guaranteed to be successful -- if the calls to
        ///     the PHCC Interface Library fail or if the device
        ///     is not currently communicating, then the controls list will remain
        ///     unpopulated.
        /// </summary>
        protected override void LoadControls()
        {
            if (_controlsLoaded)
            {
                return;
            }
            var manager = PHCCDeviceManager.GetInstance();
            if (!PHCCDeviceManager.IsDeviceAttached(this, false))
            {
                return;
            }
            _controls = PHCCDeviceManager.GetControlsOnDevice(this, false);
            _controlsLoaded = true;
        }
    }
}
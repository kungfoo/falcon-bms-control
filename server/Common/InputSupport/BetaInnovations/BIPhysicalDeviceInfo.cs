using System;

namespace Common.InputSupport.BetaInnovations
{
    /// <summary>
    ///     Represents a specific physical BetaInnovations input device
    /// </summary>
    [Serializable]
    public class BIPhysicalDeviceInfo : PhysicalDeviceInfo
    {
        /// <summary>
        /// </summary>
        public BIPhysicalDeviceInfo()
        {
        }

        /// <summary>
        ///     Constructs a BIPhysicalDeviceInfo, given a Device Path
        ///     and an (optional) alias ("Friendly name")
        ///     to use for the device
        /// </summary>
        /// <param name="devicePath">
        ///     the Windows registry device path of the BetaInnovations
        ///     input device to be represented by the newly-created object
        /// </param>
        /// <param name="alias">
        ///     a string containing a "friendly name" (alias)
        ///     to associate with the device being represented
        /// </param>
        public BIPhysicalDeviceInfo(string devicePath, string alias)
            : base(devicePath, alias)
        {
        }

        /// <summary>
        ///     Discovers the physical controls that appear on this device,
        ///     as reported by the BetaInnovations SDK, and stores them as an array
        ///     of PhysicalControlInfo objects at the instance level.
        ///     NOT guaranteed to be successful -- if the calls to
        ///     the BetaInnovations SDK fail or if the device
        ///     is not currently registered, then the controls list will remain
        ///     unpopulated.
        /// </summary>
        protected override void LoadControls()
        {
            if (_controlsLoaded)
            {
                return;
            }
            var manager = BIDeviceManager.GetInstance();
            if (!manager.IsDeviceAttached(_key.ToString(), false))
            {
                return;
            }
            _controls = manager.GetControlsOnDevice(this, false);
            _controlsLoaded = true;
        }
    }
}
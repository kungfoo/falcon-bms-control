using System;
using System.Linq;

namespace Common.InputSupport
{
    /// <summary>
    ///     Represents a specific physical input device (gaming device)
    ///     such as a joystick, gaming wheel, etc.
    /// </summary>
    [Serializable]
    public abstract class PhysicalDeviceInfo
    {
        /// <summary>
        ///     Stores a "friendly name" for this device, useful in editors
        /// </summary>
        private string _alias;

        /// <summary>
        ///     An array of PhysicalControlInfo objects, where each
        ///     element in the array represents a distinct physical control
        ///     (button, axis, or Pov control) appearing on this device
        /// </summary>
        protected PhysicalControlInfo[] _controls;

        /// <summary>
        ///     signal flag to determine if the _controls array has been populated
        ///     yet, either from a call to LoadControls() or via deserialization
        /// </summary>
        protected bool _controlsLoaded;

        /// <summary>
        ///     stores a key that represents this device uniquely on the system
        /// </summary>
        protected object _key;

        /// <summary>
        /// </summary>
        protected PhysicalDeviceInfo()
        {
        }

        /// <summary>
        ///     Constructs a PhysicalDeviceInfo, given a unique key and an (optional) alias ("Friendly name")
        ///     to use for the device
        /// </summary>
        /// <param name="key">
        ///     a key that will uniquely identify this physical
        ///     input device
        /// </param>
        /// <param name="alias">
        ///     a string containing a "friendly name" (alias)
        ///     to associate with the device being represented
        /// </param>
        protected PhysicalDeviceInfo(object key, string alias)
        {
            _key = key;
            _alias = alias;
        }

        /// <summary>
        ///     Gets/sets the "friendly name" (alias) to associate with this device.
        /// </summary>
        public string Alias
        {
            get => _alias;
            set => _alias = value;
        }

        /// <summary>
        ///     Gets an array of PhysicalControlInfo objects representing
        ///     the Axes appearing on this device.  If the physical controls
        ///     cannot be detected because the calls to the underlying device fail
        ///     or if the device is not attached to the system and its controls
        ///     have not previously been deserialized from an earlier detection,
        ///     then this method returns a null array.
        /// </summary>
        public PhysicalControlInfo[] Axes
        {
            get
            {
                if (!_controlsLoaded)
                {
                    LoadControls();
                }
                return _controls?.Where(control => control.ControlType == ControlType.Axis).ToArray();
            }
        }

        /// <summary>
        ///     Returns an array of PhysicalControlInfo objects representing
        ///     the Buttons appearing on this device.  If the physical controls
        ///     cannot be detected because the calls to the underlying device fail
        ///     or if the device is not attached to the system and its controls
        ///     have not previously been deserialized from an earlier detection,
        ///     then this method returns a null array.
        /// </summary>
        public PhysicalControlInfo[] Buttons
        {
            get
            {
                if (!_controlsLoaded)
                {
                    LoadControls();
                }
                return _controls?.Where(control => control.ControlType == ControlType.Button).ToArray();
            }
        }

        /// <summary>
        ///     Returns an array of PhysicalControlInfo objects representing
        ///     all of the input controls appearing on this device.
        ///     If the physical controls cannot be detected because the
        ///     calls to the underlying device fail or if the device is not
        ///     attached to the system and its controls
        ///     have not previously been deserialized from an earlier detection,
        ///     then this method returns a null array.
        /// </summary>
        public PhysicalControlInfo[] Controls
        {
            get
            {
                if (!_controlsLoaded)
                {
                    LoadControls();
                }
                return _controls;
            }
        }

        /// <summary>
        ///     Gets/sets the key that uniquely identifies this device
        /// </summary>
        public object Key
        {
            get => _key;
            set => _key = value;
        }

        /// <summary>
        ///     Returns an array of PhysicalControlInfo objects representing
        ///     the point-of-view controls appearing on this device.
        ///     If the physical controls cannot be detected because the
        ///     calls to the underlying device fail or if the device is not
        ///     attached to the system and its controls
        ///     have not previously been deserialized from an earlier detection,
        ///     then this method returns a null array.
        /// </summary>
        public PhysicalControlInfo[] Povs
        {
            get
            {
                if (!_controlsLoaded)
                {
                    LoadControls();
                }
                return _controls?.Where(control => control.ControlType == ControlType.Pov).ToArray();
            }
        }

        protected bool ControlsLoaded
        {
            get => _controlsLoaded;
            set => _controlsLoaded = value;
        }

        /// <summary>
        ///     Compares two objects to determine if they are equal to each other.
        /// </summary>
        /// <param name="obj">An object to compare this instance to</param>
        /// <returns>
        ///     a boolean, set to true if the specified object is
        ///     equal to this instance, or false if the specified object
        ///     is not equal.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            // safe because of the GetType check
            var pdi = (PhysicalDeviceInfo) obj;

            // use this pattern to compare value members
            if (!_key.Equals(pdi.Key)) return false;

            return true;
        }

        /// <summary>
        ///     Gets an integer (hash) representation of this object,
        ///     for use in hashtables.  If two objects are equal,
        ///     then their hashcodes should be equal as well.
        /// </summary>
        /// <returns>an integer containing a hashed representation of this object</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        ///     Gets a textual representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return "Key:" + _key;
        }

        /// <summary>
        ///     Discovers the physical controls that appear on this device,
        ///     and stores them as an array of PhysicalControlInfo objects at the instance level.
        ///     NOT guaranteed to be successful -- if the calls to
        ///     the underlying device fail or if the device
        ///     is not currently registered, then the controls list will remain
        ///     unpopulated.
        /// </summary>
        protected virtual void LoadControls()
        {
            throw new NotImplementedException();
        }
    }
}
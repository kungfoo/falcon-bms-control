using System;

namespace Common.InputSupport
{
    /// <summary>
    ///     Represents a specific physical control (a button, a Pov control, or an axis)
    ///     on a physical input device
    ///     such as a control on a joystick, driving wheel, or other
    ///     gaming device containing buttons, Pov controls, and/or axes
    /// </summary>
    [Serializable]
    public abstract class PhysicalControlInfo
    {
        /// <summary>
        ///     Stores a "friendly name" for this physical control -- useful in editors
        /// </summary>
        private string _alias;

        protected AxisType _axisType = AxisType.Unknown;

        /// <summary>
        ///     Represents the "offset" of this control in the collection of similar
        ///     controls on a device.  For example, button Number 1 on a device would
        ///     be controlNum=0; this value is largely irrelevant for axes but
        ///     *is* relevant for slider axes, where it indicates either Slider0 or Slider1.
        ///     It is also relevant for Pov controls.
        /// </summary>
        private int _controlNum = -1;

        protected ControlType _controlType = ControlType.Unknown;
        private int _hashCode;
        private bool _isDirty = true;

        /// <summary>
        ///     Stores a reference to a PhysicalDeviceInfo object representing
        ///     the physical input device on which this control appears
        /// </summary>
        private PhysicalDeviceInfo _parent;

        /// <summary>
        ///     Creates a new PhysicalControlInfo object.
        /// </summary>
        /// <param name="parent">
        ///     a PhysicalDeviceInfo object representing
        ///     the physical device on which this control appears
        /// </param>
        /// <param name="controlNum">
        ///     an integer indicating the relative zero-based
        ///     "offset" of this control in the collection of similar controls on the
        ///     same device.  Button 1 would be controlNum=0; the first slider would
        ///     similarily be controlNum=0; as would the first Pov control, etc.
        /// </param>
        /// <param name="controlType">the type of Control (button,POV, axis) this control represents</param>
        /// <param name="axisType">the type of Axis this control represents</param>
        protected PhysicalControlInfo(PhysicalDeviceInfo parent, int controlNum, ControlType controlType,
            AxisType axisType)
        {
            _parent = parent;
            _controlType = controlType;
            _axisType = axisType;
            _controlNum = controlNum;
        }

        /// <summary>
        ///     Creates a new PhysicalControlInfo object.
        /// </summary>
        /// <param name="parent">
        ///     a PhysicalDeviceInfo object representing
        ///     the physical device on which this control appears
        /// </param>
        /// <param name="controlNum">
        ///     an integer indicating the relative zero-based
        ///     "offset" of this control in the collection of similar controls on the
        ///     same device.  Button 1 would be controlNum=0; the first slider would
        ///     similarily be controlNum=0; as would the first Pov control, etc.
        /// </param>
        /// <param name="controlType">the type of Control (button,POV, axis) this control represents</param>
        /// <param name="axisType">the type of Axis this control represents</param>
        /// <param name="alias">
        ///     A string containing a "friendly name" (alias) to
        ///     associate with this control.
        /// </param>
        protected PhysicalControlInfo(PhysicalDeviceInfo parent, int controlNum, ControlType controlType,
            AxisType axisType,
            string alias)
        {
            _parent = parent;
            _controlType = controlType;
            _axisType = axisType;
            _controlNum = controlNum;
            _alias = alias;
        }

        /// <summary>
        ///     Private default constructor is effectively disabled,
        ///     meaning that that some other constructor must be used
        ///     to create an instance of this class
        /// </summary>
        protected PhysicalControlInfo()
        {
        }

        /// <summary>
        ///     Gets/sets the "friendly name" (alias) associated with this control
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                _alias = value;
                _isDirty = true;
            }
        }

        /// <summary>
        ///     Gets the type of axis represented by this control (if the control is
        ///     an axis -- otherwise, returns AxisType.Unknown
        /// </summary>
        public virtual AxisType AxisType
        {
            get
            {
                if (_axisType == AxisType.Unknown && ControlType == ControlType.Pov)
                {
                    _axisType = AxisType.Pov;
                }
                return _axisType;
            }
            set
            {
                _axisType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        ///     Gets the zero-based index of this control in the collection
        ///     of similar controls on the parent device.
        /// </summary>
        public int ControlNum
        {
            get => _controlNum;
            set
            {
                _controlNum = value;
                _isDirty = true;
            }
        }

        /// <summary>
        ///     Gets the type of this control (button, axis, Pov)
        /// </summary>
        public virtual ControlType ControlType
        {
            get => _controlType;
            set
            {
                _controlType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        ///     Gets a PhysicalDeviceInfo object representing the physical device
        ///     on which this control appears
        /// </summary>
        public PhysicalDeviceInfo Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                _isDirty = true;
            }
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
            var pc = (PhysicalControlInfo) obj;

            // use this pattern to compare value members
            if (!_parent.Key.Equals(pc._parent.Key)) return false;
            if (!AxisType.Equals(pc.AxisType)) return false;
            if (!ControlType.Equals(pc.ControlType)) return false;
            if (!_controlNum.Equals(pc._controlNum)) return false;

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
            if (!_isDirty && _hashCode != 0) return _hashCode;
            _hashCode = ToString().GetHashCode();
            _isDirty = false;
            return _hashCode;
        }

        /// <summary>
        ///     Gets a textual representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return "DeviceKey:" + _parent.Key + ":ControlNum:" + _controlNum + ":ControlType:" + ControlType +
                   ":AxisType:" + AxisType;
        }
    }
}
using System;

namespace Common.InputSupport.Phcc
{
    [Serializable]
    public class PHCCPhysicalControlInfo : PhysicalControlInfo
    {
        /// <summary>
        ///     Creates a new PHCCPhysicalControlInfo object.
        /// </summary>
        /// <param name="parent">
        ///     a PHCCPhysicalDeviceInfo object representing
        ///     the physical device on which this control appears
        /// </param>
        /// <param name="controlNum">
        ///     an integer indicating the relative zero-based
        ///     "offset" of this control in the collection of similar controls on the
        ///     same device.  Button 1 would be controlNum=0; the first slider would
        ///     similarily be controlNum=0; as would the first Pov control, etc.
        /// </param>
        /// <param name="controlType">the type of control this PHCCPhysicalDeviceInfo represents (axis, button, POV, etc.)</param>
        /// <param name="alias">a friendly name for this control to display to a user</param>
        public PHCCPhysicalControlInfo(PHCCPhysicalDeviceInfo parent, int controlNum, ControlType controlType,
            string alias)
            : base(parent, controlNum, controlType, AxisType.Unknown, alias)
        {
        }

        /// <summary>
        ///     Gets the type of axis represented by this control (if the control is
        ///     an axis -- otherwise, returns AxisType.Unknown
        /// </summary>
        public override AxisType AxisType
        {
            get
            {
                if (ControlType == ControlType.Pov)
                {
                    _axisType = AxisType.Pov;
                }
                return _axisType;
            }
        }

        /// <summary>
        ///     Gets the type of this control (button, axis, Pov)
        /// </summary>
        public override ControlType ControlType => _controlType;
    }
}
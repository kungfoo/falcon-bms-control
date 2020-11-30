using System;

namespace Common.InputSupport.BetaInnovations
{
    [Serializable]
    public class BIPhysicalControlInfo : PhysicalControlInfo
    {
        /// <summary>
        ///     Creates a new BIPhysicalControlInfo object.
        /// </summary>
        /// <param name="parent">
        ///     a BIPhysicalDeviceInfo object representing
        ///     the physical device on which this control appears
        /// </param>
        /// <param name="controlNum">
        ///     an integer indicating the relative zero-based
        ///     "offset" of this control in the collection of similar controls on the
        ///     same device.  Button 1 would be controlNum=0; the first slider would
        ///     similarily be controlNum=0; as would the first Pov control, etc.
        /// </param>
        public BIPhysicalControlInfo(BIPhysicalDeviceInfo parent, int controlNum)
            : base(parent, controlNum, ControlType.Button, AxisType.Unknown)
        {
        }

        /// <summary>
        ///     Creates a new BIPhysicalControlInfo object.
        /// </summary>
        /// <param name="parent">
        ///     a BIPhysicalDeviceInfo object representing
        ///     the physical device on which this control appears
        /// </param>
        /// <param name="controlNum">
        ///     an integer indicating the relative zero-based
        ///     "offset" of this control in the collection of similar controls on the
        ///     same device.  Button 1 would be controlNum=0; the first slider would
        ///     similarily be controlNum=0; as would the first Pov control, etc.
        /// </param>
        /// <param name="alias">
        ///     A string containing a "friendly name" (alias) to
        ///     associate with this control.
        /// </param>
        public BIPhysicalControlInfo(BIPhysicalDeviceInfo parent, int controlNum, string alias)
            : base(parent, controlNum, ControlType.Button, AxisType.Unknown, alias)
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
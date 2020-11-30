using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SlimDX.DirectInput;
namespace Common.InputSupport.DirectInput
{
    /// <summary>
    ///     Represents a specific physical DirectInput input device (gaming device)
    ///     such as a joystick, gaming wheel, etc.
    /// </summary>
    [Serializable]
    public class DIPhysicalDeviceInfo : PhysicalDeviceInfo
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DIPhysicalDeviceInfo));
        private Guid _guid = Guid.Empty;

        /// <summary>
        ///     Constructs a DIPhysicalDeviceInfo, given a DirectInput
        ///     Device Instance GUID and an (optional) alias ("Friendly name")
        ///     to use for the device
        /// </summary>
        /// <param name="guid">
        ///     a GUID containing the DirectInput
        ///     Device Instance GUID of the physical input device to be
        ///     represented by the newly-created object
        /// </param>
        /// <param name="alias">
        ///     a string containing a "friendly name" (alias)
        ///     to associate with the device being represented
        /// </param>
        public DIPhysicalDeviceInfo(Guid guid, string alias) : base(guid, alias)
        {
            _guid = guid;
        }

        /// <summary>
        /// </summary>
        private DIPhysicalDeviceInfo()
        {
        }

        public int DeviceNum { get; set; }

        public Guid Guid
        {
            get => _guid;
            set => _guid = value;
        }

        /// <summary>
        ///     Discovers the physical controls that appear on this device,
        ///     as reported by DirectInput, and stores them as an array
        ///     of PhysicalControlInfo objects at the instance level.
        ///     NOT guaranteed to be successful -- if the calls to
        ///     DirectInput fail or if the device
        ///     is not currently registered, then the controls list will remain
        ///     unpopulated.
        /// </summary>
        protected override void LoadControls()
        {
            if (ControlsLoaded)
            {
                return;
            }

            var controls = new List<PhysicalControlInfo>();
            using (var directInput = new SlimDX.DirectInput.DirectInput())
            {
                Joystick joystick = null;
                try
                {
                    joystick = new Joystick(directInput, new Guid(Key.ToString()));
                    if (joystick == null)
                    {
                        return;
                    }
                }
                catch 
                {
                    return;
                }

                var dol = joystick.GetObjects(ObjectDeviceType.Axis);
                var lastSlider = -1;
                var lastAxis = -1;
                foreach (DeviceObjectInstance doi in dol)
                    if (doi.ObjectTypeGuid == ObjectGuid.Slider)
                    {
                        lastSlider++;
                        PhysicalControlInfo control = new DIPhysicalControlInfo(this, doi, lastSlider,
                            "Slider " + (lastSlider + 1));
                        controls.Add(control);
                    }
                    else if ((doi.ObjectType & ObjectDeviceType.Axis) != 0)
                    {
                        if (!(doi.ObjectTypeGuid == ObjectGuid.XAxis ||
                              doi.ObjectTypeGuid == ObjectGuid.YAxis ||
                              doi.ObjectTypeGuid == ObjectGuid.ZAxis ||
                              doi.ObjectTypeGuid == ObjectGuid.RotationalXAxis ||
                              doi.ObjectTypeGuid == ObjectGuid.RotationalYAxis ||
                              doi.ObjectTypeGuid == ObjectGuid.RotationalZAxis))
                        {
                            continue;
                        }
                        lastAxis++;
                        var axisName = "Unknown";
                        if (doi.ObjectTypeGuid == ObjectGuid.XAxis)
                        {
                            axisName = "X Axis";
                        }
                        else if (doi.ObjectTypeGuid == ObjectGuid.YAxis)
                        {
                            axisName = "Y Axis";
                        }
                        else if (doi.ObjectTypeGuid == ObjectGuid.ZAxis)
                        {
                            axisName = "Z Axis";
                        }
                        else if (doi.ObjectTypeGuid == ObjectGuid.RotationalXAxis)
                        {
                            axisName = "X Rotation Axis";
                        }
                        else if (doi.ObjectTypeGuid == ObjectGuid.RotationalYAxis)
                        {
                            axisName = "Y Rotation Axis";
                        }
                        else if (doi.ObjectTypeGuid == ObjectGuid.RotationalZAxis)
                        {
                            axisName = "Z Rotation Axis";
                        }

                        PhysicalControlInfo control = new DIPhysicalControlInfo(this, doi, lastAxis, axisName);
                        controls.Add(control);
                    }
                var lastButton = -1;
                dol = joystick.GetObjects(ObjectDeviceType.Button);
                foreach (DeviceObjectInstance doi in dol)
                {
                    lastButton++;
                    PhysicalControlInfo control = new DIPhysicalControlInfo(this, doi, lastButton,
                        "Button " + (lastButton + 1));
                    controls.Add(control);
                }

                var lastPov = -1;
                dol = joystick.GetObjects(ObjectDeviceType.PointOfViewController);
                foreach (DeviceObjectInstance doi in dol)
                {
                    lastPov++;
                    PhysicalControlInfo control = new DIPhysicalControlInfo(this, doi, lastPov, "Hat " + (lastPov + 1));
                    controls.Add(control);
                }
                _controls = new PhysicalControlInfo[controls.Count];
                var thisControlIndex = 0;
                foreach (var control in controls)
                {
                    _controls[thisControlIndex] = control;
                    thisControlIndex++;
                }
                ControlsLoaded = true;
            }
        }
    }
}
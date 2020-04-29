using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using log4net;

namespace Common.HardwareSupport
{
    [Serializable]
    public class HardwareSupportModuleRegistry
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HardwareSupportModuleRegistry));

        [XmlArray(ElementName = "HardwareSupportModules")]
        [XmlArrayItem("Module")]
        public string[] HardwareSupportModuleTypeNames { get; set; }

        public List<IHardwareSupportModule> GetInstances()
        {
            List<IHardwareSupportModule> toReturn = null;
            foreach (var hsmTypeName in HardwareSupportModuleTypeNames)
                try
                {
                    var hsmType = Type.GetType(hsmTypeName);
                    if (hsmType == null) continue;
                    var method = hsmType.GetMethod
                    (
                        "GetInstances",
                        BindingFlags.Public
                        |
                        BindingFlags.Static
                        |
                        BindingFlags.InvokeMethod,
                        null,
                        new Type[] { },
                        null
                    );
                    if (method != null)
                    {
                        var hsmArray = method.Invoke(null, null);
                        IHardwareSupportModule[] array = hsmArray as IHardwareSupportModule[];
                        if (array == null) continue;
                        if (toReturn == null)
                        {
                            toReturn = new List<IHardwareSupportModule>();
                        }
                        toReturn.AddRange(array);
                    }
                    else
                    {
                        if (toReturn == null)
                        {
                            toReturn = new List<IHardwareSupportModule>();
                        }
                        toReturn.Add((IHardwareSupportModule) Activator.CreateInstance(hsmType));
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message, e);
                }
            return toReturn;
        }

        public static HardwareSupportModuleRegistry Load(string fileName)
        {
            return Serialization.Util.DeserializeFromXmlFile<HardwareSupportModuleRegistry>(fileName);
        }

        public void Save(string fileName)
        {
            Serialization.Util.SerializeToXmlFile(this, fileName);
        }
    }
}
using System;

namespace F4KeyFile
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute() { }
        public DescriptionAttribute(string description) { Description = description; }
        public string Description { get; set; }
    }

}

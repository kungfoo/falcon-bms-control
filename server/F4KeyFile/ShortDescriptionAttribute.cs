using System;

namespace F4KeyFile
{
    [AttributeUsage(AttributeTargets.All)]
    public class ShortDescriptionAttribute : Attribute
    {
        public ShortDescriptionAttribute() { }
        public ShortDescriptionAttribute(string shortDescription) { ShortDescription = shortDescription; }
        public string ShortDescription { get; set; }
    }

}

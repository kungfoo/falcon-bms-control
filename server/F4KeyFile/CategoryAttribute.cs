using System;

namespace F4KeyFile
{
    [AttributeUsage(AttributeTargets.All)]
    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute() { }
        public CategoryAttribute(string category) { Category = category; }
        public string Category { get; set; }
    }

}

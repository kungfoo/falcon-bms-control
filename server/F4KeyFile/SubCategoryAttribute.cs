using System;

namespace F4KeyFile
{
    [AttributeUsage(AttributeTargets.All)]
    public class SubCategoryAttribute : Attribute
    {
        public SubCategoryAttribute() { }
        public SubCategoryAttribute(string subCategory) { SubCategory = subCategory; }
        public string SubCategory { get; set; }
    }

}

using System;

namespace Common.Drawing
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ToolboxBitmapAttribute : System.Drawing.ToolboxBitmapAttribute
    {
        //
        // Summary:
        //     Initializes a new System.Drawing.ToolboxBitmapAttribute object with an image
        //     from a specified file.
        //
        // Parameters:
        //   imageFile:
        //     The name of a file that contains a 16 by 16 bitmap.
        public ToolboxBitmapAttribute(string imageFile) : base(imageFile)
        {
        }

        //
        // Summary:
        //     Initializes a new System.Drawing.ToolboxBitmapAttribute object based on a 16
        //     x 16 bitmap that is embedded as a resource in a specified assembly.
        //
        // Parameters:
        //   t:
        //     A System.Type whose defining assembly is searched for the bitmap resource.
        public ToolboxBitmapAttribute(Type t) : base(t)
        {
        }

        //
        // Summary:
        //     Initializes a new System.Drawing.ToolboxBitmapAttribute object based on a 16
        //     by 16 bitmap that is embedded as a resource in a specified assembly.
        //
        // Parameters:
        //   t:
        //     A System.Type whose defining assembly is searched for the bitmap resource.
        //
        //   name:
        //     The name of the embedded bitmap resource.
        public ToolboxBitmapAttribute(Type t, string name) : base(t, name)
        {
        }
    }
}
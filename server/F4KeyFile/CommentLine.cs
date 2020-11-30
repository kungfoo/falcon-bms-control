using System;
using System.Runtime.InteropServices;

namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class CommentLine : ILineInFile
    {
        public CommentLine() { }
        public CommentLine(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        public int LineNum { get; set; }
        public override string ToString()
        {
            var textToReturn = Text ?? string.Empty;
            if (!textToReturn.StartsWith("#") && !textToReturn.StartsWith("/"))
            {
                textToReturn = "#" + textToReturn;
            }
            return textToReturn;
        }
    }
}
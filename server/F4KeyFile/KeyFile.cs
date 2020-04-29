using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using log4net;
namespace F4KeyFile
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Serializable]
    public sealed class KeyFile
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(KeyFile));
        
        private IList<ILineInFile> _lines = new List<ILineInFile>();
        private readonly IDictionary<string, IBinding> _callbackBindings = new Dictionary<string, IBinding>();
        private Encoding _encoding = Encoding.Default;
        public KeyFile() { }

        public string FileName { get; internal set; }
        public ILineInFile[] Lines
        {
            get { return _lines.ToArray(); }
            set
            {
                _lines = value;
                UpdateIndexOfCallbacks();
            }
        }

        public Encoding Encoding
        {
            get { return _encoding;}
            set { _encoding = value ?? Encoding.Default; }
        }

        private void UpdateIndexOfCallbacks()
        {
            _callbackBindings.Clear();
            foreach (var binding in 
                            _lines.OfType<KeyBinding>().Cast<IBinding>()
                    .Union(_lines.OfType<DirectInputBinding>()))
            {
                _callbackBindings[binding.Callback] = binding;
            }
                
        }
        public IBinding GetBindingForCallback(string callback)
        {
            return _callbackBindings.ContainsKey(callback) ? _callbackBindings[callback] : null;
        }

        public static KeyFile Load(string fileName)
        {
            var encoding = Util.GetEncoding(fileName) ?? Encoding.Default;
            return Load(fileName, encoding);
        }
        public static KeyFile Load(string fileName, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            var file = new FileInfo(fileName);
            var keyFile = new KeyFile {FileName = file.FullName, _encoding = encoding};
            using (var sr = new StreamReader(file.FullName, encoding))
            {
                var lineNum = 0;
                while (!sr.EndOfStream)
                {
                    lineNum++;
                    var currentLine = sr.ReadLine();
                    if (currentLine != null)
                    {
                        var currentLineTrim = currentLine.Trim();
                        if (currentLineTrim.TrimStart().StartsWith("/") || currentLineTrim.TrimStart().StartsWith("#"))
                        {
                            keyFile._lines.Add(new CommentLine(currentLine) { LineNum = lineNum });
                            continue;
                        }
                    }

                    var tokenList = Util.Tokenize(currentLine);
                    if (tokenList == null || tokenList.Count == 0)
                    {
                        keyFile._lines.Add(new BlankLine() { LineNum = lineNum });
                        continue;
                    }

                    try
                    {
                        int token3;
                        Int32.TryParse(tokenList[2],out token3);
                        if (token3 ==-1 || token3 == -2 || token3 ==-4 || token3 == 8)
                        {
                            DirectInputBinding directInputBinding;
                            var parsed = DirectInputBinding.TryParse(currentLine, out directInputBinding);
                            if (!parsed)
                            {
                                keyFile._lines.Add(new UnparsableLine(currentLine) { LineNum = lineNum });
                                continue;
                            }
                            directInputBinding.LineNum = lineNum;
                            keyFile._lines.Add(directInputBinding);
                        }
                        else
                        {
                            KeyBinding keyBinding;
                            var parsed = KeyBinding.TryParse(currentLine, out keyBinding);
                            if (!parsed)
                            {
                                keyFile._lines.Add(new UnparsableLine(currentLine) { LineNum = lineNum });
                                continue;
                            }
                            keyBinding.LineNum = lineNum;
                            keyFile._lines.Add(keyBinding);
                        }
                    }
                    catch (Exception e)
                    {
                        keyFile._lines.Add(new UnparsableLine(currentLine) { LineNum = lineNum });
                        Log.Error($"Line {lineNum} in key file {file.FullName} could not be parsed.", e);
                    }
                }
            }
            keyFile.UpdateIndexOfCallbacks();
            return keyFile;
        }

        public void Save(string fileName)
        {
            Save(fileName, _encoding ?? Encoding.Default);
        }
        public void Save(string fileName, Encoding encoding)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            var file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
            }
            using (var fs = file.OpenWrite())
            using (var sw = new StreamWriter(fs, encoding ?? Encoding.Default))
            {
                foreach (var binding in _lines)
                {
                    sw.WriteLine(binding.ToString());
                }
                sw.Close();
                fs.Close();
            }
            FileName = fileName;
        }
        public void SendCallbackByName(string callbackName)
        {
            CallbackSender.SendKeystrokesForCallbackName(callbackName, this);
        }
        public void SendCallback(Callbacks callback)
        {
            SendCallbackByName(callback.ToString());
        }
    }
}
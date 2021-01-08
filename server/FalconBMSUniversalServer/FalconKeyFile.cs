﻿//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

 using System;
 using System.Collections.Generic;
 using System.IO;
 using System.Text;

 namespace FalconBMSUniversalServer
{
    public class FalconKeyFile
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("FalconKeyFile");
        private bool _parsed = false;
        private readonly Dictionary<string, FalconKeyCallback> _callbacks = new Dictionary<string, FalconKeyCallback>();
        private List<FalconKeyCallback> _callbackList;

        public FalconKeyFile(string keyFile)
        {
            FileName = keyFile;
        }

        #region Properties

        public string FileName { get; }

        #endregion

        private void ParseKeys(string keyFile)
        {
            if (File.Exists(keyFile))
            {
                using (StreamReader reader = File.OpenText(keyFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length > 0 && line[0] != '#')
                        {
                            string[] tokens = SplitArgument(line);

                            if (tokens.Length >= 2)
                            {
                                string callbackName = tokens[0];
                                int isKey = int.Parse(tokens[2]);
                                if (isKey == 0)
                                {
                                    FalconKeyCallback callback = new FalconKeyCallback(callbackName);
                                    callback.KeyCode = ConvertString(tokens[3]);
                                    callback.Modifiers = ConvertString(tokens[4]);
                                    callback.ComboKeyCode = ConvertString(tokens[5]);
                                    callback.ComboModifiers = ConvertString(tokens[6]);
                                    if (tokens.Length >= 9)
                                    {
                                        callback.Description = tokens[8];
                                    }
                                    if (!_callbacks.ContainsKey(callbackName) && callback.KeyCode > 0)
                                    {
                                        _callbacks.Add(callbackName, callback);
                                    }
                                }
                            }
                        }
                    }
                }
                _callbackList = new List<FalconKeyCallback>(_callbacks.Values);
                _callbackList.Sort();
                _parsed = true;
            }
        }

        private static string[] SplitArgument(String argumentString)
        {
            StringBuilder translatedArguments = new StringBuilder(argumentString).Replace("\\\"", "\r");
            bool insideQuote = false;
            for (var i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                {
                    insideQuote = !insideQuote;
                }
                if (translatedArguments[i] == ' ' && !insideQuote)
                {
                    translatedArguments[i] = '\n';
                }
            }

            string[] toReturn = translatedArguments.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = RemoveMatchingQuotes(toReturn[i]);
                toReturn[i] = toReturn[i].Replace("\r", "\"");
            }
            return toReturn;
        }

        private static string RemoveMatchingQuotes(string stringToTrim)
        {
            int firstQuoteIndex = stringToTrim.IndexOf('"');
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); //-1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }
            return stringToTrim;
        }


        private int ConvertString(string numberString)
        {
            try
            {
                if (numberString.StartsWith("0X", StringComparison.CurrentCultureIgnoreCase))
                {
                    return int.Parse(numberString.Substring(2), System.Globalization.NumberStyles.HexNumber);
                }
                return int.Parse(numberString);
            }
            catch (FormatException e)
            {
                Logger.Error("Error parsing falcon key file: {}", e);
            }

            return 0;
        }

        public List<FalconKeyCallback> Callbacks
        {
            get
            {
                if (!_parsed)
                {
                    ParseKeys(FileName);
                }

                return _callbackList;
            }
        }

        public bool HasCallback(string callbackName)
        {
            if (!_parsed)
            {
                ParseKeys(FileName);
            } 
            
            return _callbacks.ContainsKey(callbackName);
        }

        public FalconKeyCallback this[string callbackName]
        {
            get
            {
                if (!_parsed)
                {
                    ParseKeys(FileName);
                }

                return _callbacks.ContainsKey(callbackName) ? _callbacks[callbackName] : null;
            }
        }
    }
}

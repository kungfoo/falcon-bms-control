using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Strings
{
    public static class Util
    {
        public static byte[] GetBytesInDefaultEncoding(string aString)
        {
            return aString != null ? Encoding.Default.GetBytes(aString) : new byte[0];
        }

        public static List<string> Tokenize(string input)
        {
            var r = new Regex(@"[\s]+");
            var tokens = r.Split(input);
            var tokenList = new List<string>();
            foreach (var token in tokens)
            {
                if (token.Trim().Length == 0)
                {
                    continue;
                }
                if (token.EndsWith(";", System.StringComparison.Ordinal))
                {
                    var thisToken = token;
                    var tokensReplaced = 0;
                    while (thisToken.EndsWith(";", System.StringComparison.Ordinal))
                    {
                        thisToken = thisToken.Substring(0, thisToken.Length - 1);
                        tokensReplaced++;
                    }
                    tokenList.Add(thisToken);
                    for (var i = 0; i < tokensReplaced; i++)
                        tokenList.Add(";");
                }
                else if (token.StartsWith("//", System.StringComparison.Ordinal))
                {
                    var thisToken = token;
                    var tokensReplaced = 0;
                    while (thisToken.StartsWith("//", System.StringComparison.Ordinal))
                    {
                        thisToken = thisToken.Substring(2, thisToken.Length - 2);
                        tokensReplaced++;
                    }
                    for (var i = 0; i < tokensReplaced; i++)
                        tokenList.Add("//");
                    tokenList.Add(thisToken);
                }
                else
                {
                    tokenList.Add(token);
                }
            }
            return tokenList;
        }

        public static string TrimAtNull(this string toTrim)
        {
            if (toTrim == null) return null;
            var firstNull = toTrim.IndexOf('\0');
            return firstNull < 0 ? toTrim : toTrim.Substring(0, firstNull);
        }
    }
}
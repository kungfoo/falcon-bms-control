using System.Collections.Generic;
using System.Text;

namespace F4Utils.Terrain
{
    internal interface ITokenJoiner
    {
        string JoinTokens(List<string> tokens, bool omitFirstToken);
    }
    internal class TokenJoiner:ITokenJoiner
    {
        public string JoinTokens(List<string> tokens, bool omitFirstToken)
        {
            string toReturn = null;
            if (tokens != null && tokens.Count > 0)
            {
                var sb = new StringBuilder();
                var first = true;
                foreach (var st in tokens)
                {
                    if (omitFirstToken && first)
                    {
                        first = false;
                        continue;
                    }
                    sb.Append(st);
                    sb.Append(" ");
                    first = false;
                }
                toReturn = sb.ToString().Trim();
            }
            return toReturn;
        }
    }
}

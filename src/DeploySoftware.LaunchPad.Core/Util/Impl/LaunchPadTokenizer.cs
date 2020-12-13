using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public class LaunchPadTokenizer
    {
        public SortedDictionary<string, string> MatchedTokens { get; set; }
        public SortedDictionary<string, string> UnmatchedTokens { get; set; }

        public string TokenizedText { get; set; }

        public LaunchPadTokenizer()
        {
            TokenizedText = string.Empty;
            MatchedTokens = new SortedDictionary<string, string>();
            UnmatchedTokens = new SortedDictionary<string, string>();
        }

        public string Tokenize(string originalText, SortedDictionary<string, string> tokens)
        {
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(originalText), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_OriginalText);
            Guard.Against<ArgumentException>(tokens.Count == 0, DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_Tokens);
            MatchedTokens = new SortedDictionary<string, string>();
            UnmatchedTokens = new SortedDictionary<string, string>();
            Stopwatch sw;
            string modifiedText = originalText;
            foreach (var token in tokens)
            {
                var regex = new Regex(Regex.Escape(token.Key), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                modifiedText = regex.Replace(modifiedText, token.Value);
                sw = Stopwatch.StartNew();
                Match m = Regex.Match(originalText, token.Key, RegexOptions.IgnoreCase | RegexOptions.IgnoreCase);
                sw.Stop();
                if (m.Success)
                {
                    if(!MatchedTokens.ContainsKey(token.Key))
                    {
                        MatchedTokens.Add(token.Key, token.Value);
                    }
                }                    
                else
                {
                    if (!UnmatchedTokens.ContainsKey(token.Key))
                    {
                        UnmatchedTokens.Add(token.Key, token.Value);
                    }
                }
            }
            TokenizedText = modifiedText;
            return TokenizedText;
        }
    }
}

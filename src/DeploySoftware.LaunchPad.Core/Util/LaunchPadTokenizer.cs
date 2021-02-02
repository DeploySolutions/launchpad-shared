using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public class LaunchPadTokenizer
    {
        public IList<LaunchPadToken> MatchedTokens { get; set; }
        public IList<LaunchPadToken> UnmatchedTokens { get; set; }

        public string TokenizedText { get; set; }

        public LaunchPadTokenizer()
        {
            TokenizedText = string.Empty;
            MatchedTokens = new List<LaunchPadToken>();
            UnmatchedTokens = new List<LaunchPadToken>();
        }

        public string Tokenize(string originalText, IList<LaunchPadToken> tokens)
        {
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(originalText), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_OriginalText);
            Guard.Against<ArgumentException>(tokens.Count == 0, DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_Tokens);
            MatchedTokens = new List<LaunchPadToken>();
            UnmatchedTokens = new List<LaunchPadToken>();
            Stopwatch sw;
            string modifiedText = originalText;
            foreach (var token in tokens)
            {
                var regex = new Regex(Regex.Escape(token.ToString()), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                sw = Stopwatch.StartNew();
                bool succeeded = Regex.IsMatch(originalText, token.ToString(), RegexOptions.IgnoreCase);
                sw.Stop();
                if (succeeded)
                {
                    if (string.IsNullOrEmpty(token.Value))
                    {
                        modifiedText = regex.Replace(modifiedText, token.DefaultValue);
                    }
                    else
                    {
                        modifiedText = regex.Replace(modifiedText, token.Value);
                    }
                    if (!MatchedTokens.Contains(token))
                    {
                        MatchedTokens.Add(token);
                    }
                }                    
                else
                {
                    if (!UnmatchedTokens.Contains(token))
                    {
                        UnmatchedTokens.Add(token);
                    }
                }
            }
            TokenizedText = modifiedText;
            return TokenizedText;
        }
    }
}

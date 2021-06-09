using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public class LaunchPadTokenizer
    {
        public IDictionary<string, LaunchPadToken> MatchedTokens { get; set; }
        public IDictionary<string, LaunchPadToken> UnmatchedTokens { get; set; }

        public string TokenizedText { get; set; }

        public LaunchPadTokenizer()
        {
            TokenizedText = string.Empty; 
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }

        public string Tokenize(string originalText, IDictionary<string, LaunchPadToken> tokens)
        {
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(originalText), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_OriginalText);
            Guard.Against<ArgumentException>(tokens.Count == 0, DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_Tokens);
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            Stopwatch sw;
            string modifiedText = originalText;
            foreach (var token in tokens.Values)
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
                    if (!MatchedTokens.ContainsKey(token.Name))
                    {
                        MatchedTokens.Add(token.Name,token);
                    }
                }                    
                else
                {
                    if (!UnmatchedTokens.ContainsKey(token.Name))
                    {
                        UnmatchedTokens.Add(token.Name,token);
                    }
                }
            }
            TokenizedText = modifiedText;
            return TokenizedText;
        }
    }
}

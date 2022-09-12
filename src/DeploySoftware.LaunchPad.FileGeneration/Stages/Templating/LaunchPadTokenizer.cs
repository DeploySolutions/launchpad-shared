using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
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

        public string Tokenize(string originalText, IDictionary<string, LaunchPadToken> tokens, bool shouldMatchTokenValue = false, ILogger logger = null, bool shouldLogTokens = false)
        {
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(originalText), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_OriginalText);
            Guard.Against<ArgumentException>(tokens.Count == 0, DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_Tokens);
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            Stopwatch sw;
            string modifiedText = originalText;
            if(logger == null)
            {
                logger = NullLogger.Instance;
            }
            // Token examples:
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name}}
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name|tags:a=x;}}
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name|tags:a=x;k2=v2;}}
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name|tags:a=x;k2=v2;|v:this}}
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name|tags:a=x;k2=v2;|v:that}}
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name|tags:a=x;k2=v2;|v:that|dv:that}}
            //{{p:dss|n:dss_comp_webportal_backend_Solution_Details_Name|tags:b=y;k2=v2;}}
            //{{p:fabrikant|n:dss_comp_webportal_backend_Solution_Details_Name|tags:a=y;k2=v4;}}
            //
            foreach (var token in tokens.Values)
            {
                StringBuilder sbRegExp = new StringBuilder();
                sbRegExp.Append(@"\{\{p:");
                sbRegExp.Append(token.Prefix);
                sbRegExp.Append(@"\|n:");
                sbRegExp.Append(token.Name);

                // specific filters?
                if (token.Tags != null && token.Tags.Count >0)
                {
                    sbRegExp.Append(@"((?:\|tags:((.*((?:((");
                    StringBuilder sbTagTokenFormat = new StringBuilder();
                    // generate the token tags format for the kvps
                    foreach(var tag in token.Tags)
                    {
                        sbRegExp.Append(EscapeTextForRegex(tag.Key));
                        sbRegExp.Append("=");
                        sbRegExp.Append(EscapeTextForRegex(tag.Value));
                        sbRegExp.Append(";");
                    }
                    sbRegExp.Append(@".*?)+))))+)))"); // match the token tags
                }else if (shouldMatchTokenValue && !string.IsNullOrEmpty(token.Value))
                {
                    sbRegExp.Append(@"(?:\|v:(("); // start of the v: element
                    sbRegExp.Append(EscapeTextForRegex(token.Value));
                    sbRegExp.Append(@".*?)+))"); // ending of the v: element
                }
                else if (!string.IsNullOrEmpty(token.DefaultValue))
                {
                    sbRegExp.Append(@"(?:\|dv:(("); // start of the dv: element
                    sbRegExp.Append(EscapeTextForRegex(token.DefaultValue));
                    sbRegExp.Append(@".*?)+))"); // ending of the dv: element
                } 
                else // don't filter, just match on the remaining characters
                {
                    sbRegExp.Append(@"((.*?)+))");
                }
                sbRegExp.Append(@"\}\}");
                //string regexPattern = Regex.Escape(sbRegExp.ToString());
                string regexPattern = sbRegExp.ToString();
                sw = Stopwatch.StartNew();
                bool succeeded = Regex.IsMatch(originalText, regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                sw.Stop();
                if (succeeded) // do the RegEx replacement
                {
                    if(shouldLogTokens)
                    {
                        logger.Debug(string.Format("LaunchPadTokenizer.Tokenize() => Token '{0}' regex succeeded with pattern '{1}'",
                            token.Name,
                            regexPattern
                        ));
                    }
                    
                    var regex = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
                    if (shouldLogTokens)
                    {
                        logger.Debug(string.Format("LaunchPadTokenizer.Tokenize() => Token '{0}' regex failed to match pattern '{1}'",
                            token.Name,
                            regexPattern
                        ));
                    }
                    
                    if (!UnmatchedTokens.ContainsKey(token.Name))
                    {
                        UnmatchedTokens.Add(token.Name,token);
                    }
                }
            }
            TokenizedText = modifiedText;
            if (shouldLogTokens)
            {
                logger.Debug(string.Format("LaunchPadTokenizer.Tokenize() => modified text is '{0}'.", modifiedText));
            }
            
            return TokenizedText;
        }

        /// <summary>
        /// Escapes RegEx characters from provided text to ensure the resulting Regex pattern is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string EscapeTextForRegex(string value)
        {
            IList<string> escapeCharacters = new List<string>();
            escapeCharacters.Add("(");
            escapeCharacters.Add(")");
            escapeCharacters.Add("[");
            escapeCharacters.Add("]");
            escapeCharacters.Add("/");
            escapeCharacters.Add("{");
            escapeCharacters.Add("}");
            escapeCharacters.Add("*");
            escapeCharacters.Add("+");
            escapeCharacters.Add("?");
            escapeCharacters.Add(".");
            escapeCharacters.Add("$");

            StringBuilder sb = new StringBuilder(value);
            foreach (string unwanted in escapeCharacters)
            {
                sb.Replace(unwanted, '\\' + unwanted);
            }
            return sb.ToString();
        }
    }
}

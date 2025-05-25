// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadTokenizer.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Deploy.LaunchPad.Core.Services
{
    /// <summary>
    /// Class LaunchPadTokenizer.
    /// </summary>
    public partial class LaunchPadTokenService : LaunchPadServiceBase, ILaunchPadTokenService
    {
      
        /// <summary>
        /// Gets or sets the matched tokens.
        /// </summary>
        /// <value>The matched tokens.</value>
        public IDictionary<string, LaunchPadToken> MatchedTokens { get; set; }
        /// <summary>
        /// Gets or sets the unmatched tokens.
        /// </summary>
        /// <value>The unmatched tokens.</value>
        public IDictionary<string, LaunchPadToken> UnmatchedTokens { get; set; }

        /// <summary>
        /// Gets or sets the tokenized text.
        /// </summary>
        /// <value>The tokenized text.</value>
        public string TokenizedText { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTokenService"/> class.
        /// </summary>
        public LaunchPadTokenService() : base()
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("Token Service {0} ", id));
            Description = new ElementDescription(string.Format("Token Service {0} ", id));
            TokenizedText = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTokenService"/> class.
        /// </summary>
        public LaunchPadTokenService(ILogger logger) : base(logger)
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("Token Service {0} ", id));
            Description = new ElementDescription(string.Format("Token Service {0} ", id));
            TokenizedText = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTokenService"/> class.
        /// </summary>
        public LaunchPadTokenService(ILogger logger, string name) : base(logger, name)
        {          
            TokenizedText = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTokenService"/> class.
        /// </summary>
        public LaunchPadTokenService(ILogger logger, string name, string description) : base(logger, name, description)
        {
            TokenizedText = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }

        /// <summary>
        /// Tokenizes the specified original text.
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="shouldMatchTokenValue">if set to <c>true</c> [should match token value].</param>
        /// <param name="logger">The logger.</param>
        /// <param name="shouldLogTokens">if set to <c>true</c> [should log tokens].</param>
        /// <returns>System.String.</returns>
        public string Tokenize(string originalText, IDictionary<string, LaunchPadToken> tokens, TokenMatchingStrategy tokenMatching = TokenMatchingStrategy.IgnoreTokenValuesWhenMatching, TokenLoggingStrategy shouldLogTokens = TokenLoggingStrategy.DoNotLogTokenMatching)
        {
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(originalText), Deploy_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_OriginalText);
            Guard.Against<ArgumentException>(tokens.Count == 0, Deploy_LaunchPad_Core_Resources.Guard_LaunchPadTokenizer_ArgumentException_Tokens);
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            UnmatchedTokens = new Dictionary<string, LaunchPadToken>(comparer);
            Stopwatch sw;
            string modifiedText = originalText;
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
                if (token.Tags != null && token.Tags.Count > 0)
                {
                    sbRegExp.Append(@"((?:\|tags:((.*((?:((");
                    StringBuilder sbTagTokenFormat = new StringBuilder();
                    // generate the token tags format for the kvps
                    foreach (var tag in token.Tags)
                    {
                        sbRegExp.Append(EscapeTextForRegex(tag.Key));
                        sbRegExp.Append("=");
                        sbRegExp.Append(EscapeTextForRegex(tag.Value));
                        sbRegExp.Append(";");
                    }
                    sbRegExp.Append(@".*?)+))))+)))"); // match the token tags
                }
                else if (tokenMatching == TokenMatchingStrategy.ConsiderTokenValuesWhenMatching && !string.IsNullOrEmpty(token.Value))
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
                else // don't filter, just close out the name element and match on the remaining characters
                {
                    sbRegExp.Append(@"\|((.*?)+)");
                }
                sbRegExp.Append(@"\}\}");
                //string regexPattern = Regex.Escape(sbRegExp.ToString());
                string regexPattern = sbRegExp.ToString();
                sw = Stopwatch.StartNew();
                bool succeeded = Regex.IsMatch(originalText, regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                sw.Stop();
                if (succeeded) // do the RegEx replacement
                {
                    if (shouldLogTokens == TokenLoggingStrategy.LogTokenMatching)
                    {
                        Logger.Debug(string.Format("LaunchPadTokenizer.Tokenize() => Token '{0}' regex succeeded with pattern '{1}'",
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
                        MatchedTokens.Add(token.Name, token);
                    }
                }
                else
                {
                    if (shouldLogTokens == TokenLoggingStrategy.LogTokenMatching)
                    {
                        Logger.Debug(string.Format("LaunchPadTokenizer.Tokenize() => Token '{0}' regex failed to match pattern '{1}'",
                            token.Name,
                            regexPattern
                        ));
                    }

                    if (!UnmatchedTokens.ContainsKey(token.Name))
                    {
                        UnmatchedTokens.Add(token.Name, token);
                    }
                }
            }
            TokenizedText = modifiedText;
            if (shouldLogTokens == TokenLoggingStrategy.LogTokenMatching)
            {
                Logger.Debug(string.Format("LaunchPadTokenizer.Tokenize() => modified text is '{0}'.", modifiedText));
            }

            return TokenizedText;
        }

        /// <summary>
        /// Escapes RegEx characters from provided text to ensure the resulting Regex pattern is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Find a token with the given name in the text. If the token is found, return the token, otherwise return null.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tokenName"></param>
        /// <param name="tokenPattern"></param>
        /// <returns></returns>
        public LaunchPadToken FindTokenWithName(string text, string tokenName, string tokenPattern = @"\{\{p:.*?\|\}\}", TokenLoggingStrategy shouldLogTokens = TokenLoggingStrategy.DoNotLogTokenMatching)
        {
            LaunchPadToken token = null;
            var comparer = StringComparer.OrdinalIgnoreCase;
            MatchCollection matches = Regex.Matches(text, tokenPattern);
            foreach (Match match in matches)
            {
                if (match.Value.ToLower().Equals(tokenName.ToLower()))
                {
                    token = new LaunchPadToken(match.Value);
                    if(shouldLogTokens == TokenLoggingStrategy.LogTokenMatching)
                    {
                        Logger.Debug(string.Format("LaunchPadTokenizer.FindTokenWithName() => Token '{0}' was found in text '{1}'",
                                token.Name,
                                text
                        ));
                    }
                }
            }
            if(shouldLogTokens == TokenLoggingStrategy.LogTokenMatching && token == null)
            {                
                Logger.Debug(string.Format("LaunchPadTokenizer.FindTokenWithName() => No token with name '{0}' was found in text '{1}' using regex pattern {2}.",
                            token.Name,
                            text,
                            tokenPattern
                ));
            }
            return token;
        }

        /// <summary>
        /// Find all valid tokens in the text. Return a dictionary of tokens with the token name as the key.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tokenPattern"></param>
        /// <returns></returns>
        public IDictionary<string, LaunchPadToken> FindTokensInText(string text, string tokenPattern = @"\{\{p:.*?\|\}\}", TokenLoggingStrategy shouldLogTokens = TokenLoggingStrategy.DoNotLogTokenMatching)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>(comparer);
            MatchCollection matches = Regex.Matches(text, tokenPattern);
            foreach (Match match in matches)
            {
                LaunchPadToken token = new LaunchPadToken(match.Value);
                bool wasAdded = tokens.TryAdd(token.Name, token);
                if(shouldLogTokens == TokenLoggingStrategy.LogTokenMatching && wasAdded)
                {
                    Logger.Debug(string.Format("LaunchPadTokenizer.FindTokensInText() => Token '{0}' was found in text '{1}' and added to dictionary.",
                            token.Name,
                            text
                    ));
                }
            }
            return tokens;
        }
    }

    /// <summary>
    /// Token matching can be done in two ways. By default, the token's value is ignored when checking for a match. 
    /// If you want to consider (require) the token's value is present when checking for a match, set this to ConsiderTokenValuesWhenMatching.
    /// </summary>
    public enum TokenMatchingStrategy
    {
        IgnoreTokenValuesWhenMatching = 0, // when checking for token matches, ignore what's currently in the token's Value field. (the default)
        ConsiderTokenValuesWhenMatching = 1, // when checking for token matches, only consider a match if what is in the token's Value field is also present in the token in the source text.        
    }

    /// <summary>
    /// Token matching can generate a lot of logging noise and incur processing overhead. By default, logging is disabled. 
    /// It can be enabled if you want to verify unexpected matching results or errors. If enabled, it uses the standard logging engine configuration.
    /// </summary>
    public enum TokenLoggingStrategy
    {
        DoNotLogTokenMatching = 0, // do not log any token matching activities. Calling methods can do any logging they need. (the default)
        LogTokenMatching = 1 // log all token matching activities 
        

    }
}

// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadTokenService.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Util;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Services
{
    public partial interface ILaunchPadTokenService : ILaunchPadService
    {
        public IDictionary<string, LaunchPadToken> MatchedTokens { get; set; }
        public string TokenizedText { get; set; }
        public IDictionary<string, LaunchPadToken> UnmatchedTokens { get; set; }

        public IDictionary<string, LaunchPadToken> FindTokensInText(string text, string tokenPattern = "\\{\\{p:.*?\\|\\}\\}", TokenLoggingStrategy shouldLogTokens = TokenLoggingStrategy.DoNotLogTokenMatching);
        public LaunchPadToken FindTokenWithName(string text, string tokenName, string tokenPattern = "\\{\\{p:.*?\\|\\}\\}", TokenLoggingStrategy shouldLogTokens = TokenLoggingStrategy.DoNotLogTokenMatching);
        public string Tokenize(string originalText, IDictionary<string, LaunchPadToken> tokens, TokenMatchingStrategy tokenMatching = TokenMatchingStrategy.IgnoreTokenValuesWhenMatching, TokenLoggingStrategy shouldLogTokens = TokenLoggingStrategy.DoNotLogTokenMatching);
    }
}
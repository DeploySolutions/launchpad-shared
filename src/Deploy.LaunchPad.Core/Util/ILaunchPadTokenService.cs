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
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Application;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Util
{
    public interface ILaunchPadTokenService : ILaunchPadSystemIntegrationService
    {
        IDictionary<string, LaunchPadToken> MatchedTokens { get; set; }
        string TokenizedText { get; set; }
        IDictionary<string, LaunchPadToken> UnmatchedTokens { get; set; }

        IDictionary<string, LaunchPadToken> FindTokensInText(string text, string tokenPattern = "\\{\\{p:.*?\\|\\}\\}", bool shouldLogTokens = false);
        LaunchPadToken FindTokenWithName(string text, string tokenName, string tokenPattern = "\\{\\{p:.*?\\|\\}\\}", bool shouldLogTokens = false);
        string Tokenize(string originalText, IDictionary<string, LaunchPadToken> tokens, bool shouldMatchTokenValue = false, bool shouldLogTokens = false);
    }
}
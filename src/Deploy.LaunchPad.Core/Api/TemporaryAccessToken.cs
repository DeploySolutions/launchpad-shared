// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-17-2023
// ***********************************************************************
// <copyright file="TemporaryAccessToken.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text.Json.Serialization;

namespace Deploy.LaunchPad.Core.Api
{
    /// <summary>
    /// Class TemporaryAccessToken.
    /// </summary>
    public partial class TemporaryAccessToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>The expires in.</value>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the expiry date time.
        /// </summary>
        /// <value>The expiry date time.</value>
        public DateTime ExpiryDateTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryAccessToken"/> class.
        /// </summary>
        public TemporaryAccessToken()
        {

        }

    }
}

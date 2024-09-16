// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-19-2023
// ***********************************************************************
// <copyright file="IApiHelper.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Api
{
    /// <summary>
    /// Interface IApiHelper
    /// </summary>
    public partial interface IApiHelper
    {
        /// <summary>
        /// Gets or sets the API base URL.
        /// </summary>
        /// <value>The API base URL.</value>
        string ApiBaseUrl { get; set; }
        /// <summary>
        /// Gets or sets the API rest client.
        /// </summary>
        /// <value>The API rest client.</value>
        RestClient ApiRestClient { get; set; }
        /// <summary>
        /// Gets or sets the default version.
        /// </summary>
        /// <value>The default version.</value>
        string DefaultVersion { get; set; }
        /// <summary>
        /// Gets or sets the o authentication base URL.
        /// </summary>
        /// <value>The o authentication base URL.</value>
        string OAuthBaseUrl { get; set; }
        /// <summary>
        /// Gets or sets the o authentication client.
        /// </summary>
        /// <value>The o authentication client.</value>
        RestClient OAuthClient { get; set; }
        /// <summary>
        /// Gets or sets the o authentication token endpoint.
        /// </summary>
        /// <value>The o authentication token endpoint.</value>
        string OAuthTokenEndpoint { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        TemporaryAccessToken Token { get; set; }

        /// <summary>
        /// Gets the o authentication token using secret credentials.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="scopes">The scopes.</param>
        /// <returns>Task&lt;TemporaryAccessToken&gt;.</returns>
        Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null);
        /// <summary>
        /// Makes the API request.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="causationId">The causation identifier.</param>
        /// <returns>RestResponse.</returns>
        RestResponse MakeApiRequest(string secretArn, RestRequest request, string correlationId = "", string causationId = "");

        /// <summary>
        /// Makes the API request asynchronous.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="request">The request.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="causationId">The causation identifier.</param>
        /// <returns>Task&lt;RestResponse&gt;.</returns>
        Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string correlationId = "", string causationId = "");

    }
}
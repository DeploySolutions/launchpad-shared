// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsApiGatewayHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.APIGateway;
using Deploy.LaunchPad.Core.Api;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Interface IAwsApiGatewayHelper
    /// Extends the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.APIGateway.AmazonAPIGatewayConfig}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.APIGateway.AmazonAPIGatewayConfig}" />
    public partial interface IAwsApiGatewayHelper : IAwsHelper<AmazonAPIGatewayConfig>
    {
        /// <summary>
        /// Gets or sets the API base URI.
        /// </summary>
        /// <value>The API base URI.</value>
        Uri ApiBaseUri { get; set; }
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
        /// Gets or sets the o authentication base URI.
        /// </summary>
        /// <value>The o authentication base URI.</value>
        Uri OAuthBaseUri { get; set; }
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
        /// <returns>TemporaryAccessToken.</returns>
        public TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null);

        /// <summary>
        /// Gets the o authentication token using secret credentials asynchronous.
        /// </summary>
        /// <param name="vault">The vault.</param>
        /// <param name="scopes">The scopes.</param>
        /// <returns>Task&lt;TemporaryAccessToken&gt;.</returns>
        public Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentialsAsync(AwsSecretVault vault, IList<string> scopes = null);

        /// <summary>
        /// Makes the API request.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="request">The request.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>RestResponse.</returns>
        RestResponse MakeApiRequest(string secretArn, RestRequest request, string requestId = "", string correlationId = "");
        /// <summary>
        /// Makes the API request asynchronous.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="request">The request.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>Task&lt;RestResponse&gt;.</returns>
        Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = "", string correlationId = "");
    }
}
// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsApiGatewayHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.APIGateway;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Api;
using Deploy.LaunchPad.Util;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Class AwsApiGatewayHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.APIGateway.AmazonAPIGatewayConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.IAwsApiGatewayHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.APIGateway.AmazonAPIGatewayConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsApiGatewayHelper" />
    public partial class AwsApiGatewayHelper : AwsHelperBase<AmazonAPIGatewayConfig>, IAwsApiGatewayHelper
    {

        /// <summary>
        /// Gets or sets the o authentication base URI.
        /// </summary>
        /// <value>The o authentication base URI.</value>
        public Uri OAuthBaseUri { get; set; }
        /// <summary>
        /// Gets or sets the o authentication token endpoint.
        /// </summary>
        /// <value>The o authentication token endpoint.</value>
        public string OAuthTokenEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the API base URI.
        /// </summary>
        /// <value>The API base URI.</value>
        public Uri ApiBaseUri { get; set; }

        /// <summary>
        /// Gets or sets the default version.
        /// </summary>
        /// <value>The default version.</value>
        public string DefaultVersion { get; set; }

        /// <summary>
        /// Gets or sets the o authentication client.
        /// </summary>
        /// <value>The o authentication client.</value>
        [JsonIgnore]

        public RestClient OAuthClient { get; set; }

        /// <summary>
        /// Gets or sets the API rest client.
        /// </summary>
        /// <value>The API rest client.</value>
        [JsonIgnore]
        public RestClient ApiRestClient { get; set; }


        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        [JsonIgnore]
        public TemporaryAccessToken Token { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayHelper"/> class.
        /// </summary>
        protected AwsApiGatewayHelper() : base()
        {
            OAuthTokenEndpoint = string.Empty;
            DefaultVersion = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="apiGatewayBaseUri">The API gateway base URI.</param>
        public AwsApiGatewayHelper(ILogger logger, string awsRegionEndpointName, Uri apiGatewayBaseUri) : base(logger, awsRegionEndpointName)
        {
            ApiBaseUri = apiGatewayBaseUri;
            OAuthTokenEndpoint = string.Empty;
            DefaultVersion = string.Empty;
            ApiRestClient = new RestClient(apiGatewayBaseUri);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="apiGatewayBaseUri">The API gateway base URI.</param>
        /// <param name="oAuthBaseUri">The o authentication base URI.</param>
        /// <param name="oAuthTokenEndpoint">The o authentication token endpoint.</param>
        /// <param name="defaultApiVersion">The default API version.</param>
        public AwsApiGatewayHelper(ILogger logger, string awsRegionEndpointName, Uri apiGatewayBaseUri, Uri oAuthBaseUri, string oAuthTokenEndpoint, string defaultApiVersion) : base(logger, awsRegionEndpointName)
        {
            OAuthTokenEndpoint = oAuthTokenEndpoint;
            DefaultVersion = defaultApiVersion;
            OAuthBaseUri = oAuthBaseUri;
            ApiBaseUri = apiGatewayBaseUri;

            // set the REST clients
            OAuthClient = new RestClient(oAuthBaseUri);
            ApiRestClient = new RestClient(apiGatewayBaseUri);
        }

        /// <summary>
        /// Gets the o authentication token using secret credentials.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="scopes">The scopes.</param>
        /// <returns>TemporaryAccessToken.</returns>
        public virtual TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(string arn, IList<string> scopes = null)
        {
            AwsSecretProvider provider = new AwsSecretProvider(Region.SystemName, AwsProfileName, ShouldUseLocalAwsProfile);
            AwsSecretVault vault = (AwsSecretVault)provider.GetSecretVaultByVaultId(arn, "AwsApiGatewayHelper.GetOAuthTokenUsingSecretCredentials(string arn, IList<string> scopes = null)");
            return GetOAuthTokenUsingSecretCredentialsAsync(vault, scopes).Result;
        }

        /// <summary>
        /// Gets the o authentication token using secret credentials.
        /// </summary>
        /// <param name="vault">The vault.</param>
        /// <param name="scopes">The scopes.</param>
        /// <returns>TemporaryAccessToken.</returns>
        public virtual TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(AwsSecretVault vault, IList<string> scopes = null)
        {
            return GetOAuthTokenUsingSecretCredentialsAsync(vault, scopes).Result;
        }

        /// <summary>
        /// Request and returns the OAuth token from the information which is stored in the given secret
        /// </summary>
        /// <param name="vault">The vault.</param>
        /// <param name="scopes">The scopes.</param>
        /// <returns>A TemporaryAccessToken object</returns>
        public async virtual Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentialsAsync(AwsSecretVault vault, IList<string> scopes = null)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(vault.VaultId), Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(OAuthClient == null, Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Getting, OAuthTokenEndpoint, OAuthBaseUri.ToString(), vault.VaultId));
            TemporaryAccessToken token = null;

            string accessToken = string.Empty;
            AwsSecretProvider provider = new AwsSecretProvider(Region.SystemName, AwsProfileName, ShouldUseLocalAwsProfile);
            string secretJson = await provider.GetJsonFromSecretVaultAsync(vault, "AwsApiGatewayHelper.GetOAuthTokenUsingSecretCredentialsAsync()");
            dynamic secret = JsonConvert.DeserializeObject(secretJson);
            string apiGatewayClientId = secret.apiGatewayClientId;
            Guard.Against<InvalidOperationException>(String.IsNullOrEmpty(apiGatewayClientId), "apiGatewayClientId cannot be empty");
            string apiGatewayClientSecret = secret.apiGatewayClientSecret;
            Guard.Against<InvalidOperationException>(String.IsNullOrEmpty(apiGatewayClientSecret), "apiGatewayClientSecret cannot be empty");

            // request the temporary token from the oAuth base url and the token endpoint
            var oAuthRequest = new RestRequest(OAuthTokenEndpoint, Method.Post);

            // set the headers including the authorization credentials from the secret
            StringBuilder sbGrant = new StringBuilder();
            sbGrant.Append("grant_type=client_credentials&client_id=");
            sbGrant.Append(apiGatewayClientId);
            sbGrant.Append("&client_secret=");
            sbGrant.Append(apiGatewayClientSecret);
            if (scopes != null)
            {
                sbGrant.Append("&scope=");
                foreach (var scope in scopes)
                {
                    sbGrant.Append(scope);
                    sbGrant.Append(' ');
                }
            }
            oAuthRequest.AddHeader("cache-control", "no-cache");
            oAuthRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            oAuthRequest.AddParameter("application/x-www-form-urlencoded", sbGrant.ToString(), ParameterType.RequestBody);

            // request the token
            RestResponse oAuthResponse = await OAuthClient.ExecuteAsync(oAuthRequest);
            if (oAuthResponse.IsSuccessful)
            {

                // read the content
                string oAuthResponseContent = oAuthResponse.Content;
                dynamic oAuthResponseJson = JsonConvert.DeserializeObject(oAuthResponseContent);

                token = new TemporaryAccessToken();
                token.AccessToken = oAuthResponseJson.access_token;
                token.TokenType = oAuthResponseJson.token_type;

                // calculate when it will expire.
                string expiresIn = oAuthResponseJson.expires_in;
                token.ExpiresIn = Int32.Parse(expiresIn);
                DateTime expiryTime = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
                token.ExpiryDateTime = expiryTime;

                Logger.Debug(string.Format(Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_AccessToken, token.TokenType, token.ExpiresIn, token.AccessToken));

            }
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Got, OAuthTokenEndpoint, OAuthBaseUri.ToString(), vault.VaultId));

            return token;
        }


        /// <summary>
        /// Makes the API request.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="request">The request.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>RestResponse.</returns>
        public virtual RestResponse MakeApiRequest(string secretArn, RestRequest request, string requestId = "", string correlationId = "")
        {
            return MakeApiRequestAsync(secretArn, request, requestId, correlationId).Result;
        }


        /// <summary>
        /// Make API request as an asynchronous operation.
        /// </summary>
        /// <param name="secretArn">The secret arn.</param>
        /// <param name="request">The request.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>A Task&lt;RestResponse&gt; representing the asynchronous operation.</returns>
        public async virtual Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = "", string correlationId = "")
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(ApiRestClient == null, Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(request.Resource), Deploy_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_Request_Resource_Is_NullOrEmpty);
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executing, request.Method.ToString(), ApiBaseUri.ToString(), request.Resource));

            if (Token == null)
            {
                AwsSecretProvider provider = new AwsSecretProvider(Region.SystemName, AwsProfileName, ShouldUseLocalAwsProfile);
                AwsSecretVault vault = (AwsSecretVault)provider.GetSecretVaultByVaultId(secretArn, "AwsApiGatewayHelper.MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = \"\", string correlationId = \"\")");
                Token = await GetOAuthTokenUsingSecretCredentialsAsync(vault);
                // TODO save the token in the secret

            }
            request.AddHeader("authorization", "Bearer " + Token);

            // add the Correlation ID header, if it is a request transaction
            if (!string.IsNullOrEmpty(correlationId))
            {
                request.AddHeader("X-Correlation-ID", correlationId);
            }

            // add a unique requestId header to the receiving service
            if (string.IsNullOrEmpty(requestId))
            {
                requestId = Guid.NewGuid().ToString();
            }
            request.AddHeader("X-Request-ID", requestId);

            // make the request to the API gateway
            RestResponse response = await ApiRestClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Logger.Info(string.Format("Request succeeded. Status code: {0}. CorrelationId: {1}", response.StatusCode, correlationId));
            }
            else
            {
                Logger.Error(string.Format("Request failed with status code {0}. Reason: {1}. CorrelationId: {2}",
                                response.StatusCode,
                                response.ErrorException.Message,
                                correlationId),
                        response.ErrorException)
                ;
            }
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executed, request.Method.ToString(), ApiBaseUri.ToString(), request.Resource, response.StatusCode));
            return response;
        }

    }
}

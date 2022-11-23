using Amazon.APIGateway;
using Castle.Core.Internal;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Api;
using DeploySoftware.LaunchPad.Core.Config;
using DeploySoftware.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class AwsApiGatewayHelper : AwsHelperBase<AmazonAPIGatewayConfig>, IAwsApiGatewayHelper
    {

        public Uri OAuthBaseUri { get; set; }
        public string OAuthTokenEndpoint { get; set; }

        public Uri ApiBaseUri { get; set; }

        public string DefaultVersion { get; set; }

        [JsonIgnore]

        public RestClient OAuthClient { get; set; }

        [JsonIgnore]
        public RestClient ApiRestClient { get; set; }


        [JsonIgnore]
        public TemporaryAccessToken Token { get; set; }

        protected AwsApiGatewayHelper() : base()
        {
            OAuthTokenEndpoint = string.Empty;
            DefaultVersion = string.Empty;
        }

        public AwsApiGatewayHelper(ILogger logger, string awsRegionEndpointName, Uri apiGatewayBaseUri) : base(logger, awsRegionEndpointName)
        {
            ApiBaseUri = apiGatewayBaseUri;
            OAuthTokenEndpoint = string.Empty;
            DefaultVersion = string.Empty;
            ApiRestClient = new RestClient(apiGatewayBaseUri);
        }

        public AwsApiGatewayHelper(ILogger logger, string awsRegionEndpointName,  Uri apiGatewayBaseUri, Uri oAuthBaseUri, string oAuthTokenEndpoint,  string defaultApiVersion) : base(logger, awsRegionEndpointName)
        {
            OAuthTokenEndpoint = oAuthTokenEndpoint;
            DefaultVersion = defaultApiVersion;
            OAuthBaseUri = oAuthBaseUri;
            ApiBaseUri = apiGatewayBaseUri;

            // set the REST clients
            OAuthClient = new RestClient(oAuthBaseUri);
            ApiRestClient = new RestClient(apiGatewayBaseUri);
        }

        public virtual TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(string arn, IList<string> scopes = null)
        {
            AwsSecretProvider provider = new AwsSecretProvider(Region.SystemName, AwsProfileName, ShouldUseLocalAwsProfile);
            AwsSecretVault vault = (AwsSecretVault)provider.GetSecretVaultByVaultId(arn, "AwsApiGatewayHelper.GetOAuthTokenUsingSecretCredentials(string arn, IList<string> scopes = null)");
            return GetOAuthTokenUsingSecretCredentialsAsync(vault, scopes).Result;
        }

        public virtual TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(AwsSecretVault vault, IList<string> scopes = null)
        {
            return GetOAuthTokenUsingSecretCredentialsAsync(vault, scopes).Result;
        }

        /// <summary>
        /// Request and returns the OAuth token from the information which is stored in the given secret
        /// </summary>
        /// <param name="secretArn">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A TemporaryAccessToken object</returns>
        public async virtual Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentialsAsync(AwsSecretVault vault, IList<string> scopes = null)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(vault.VaultId), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(OAuthClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Getting, OAuthTokenEndpoint, OAuthBaseUri.ToString(), vault.VaultId));
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
            var oAuthRequest = new RestRequest(OAuthTokenEndpoint,Method.Post);

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

                Logger.Debug(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_AccessToken, token.TokenType, token.ExpiresIn, token.AccessToken));

            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Got, OAuthTokenEndpoint, OAuthBaseUri.ToString(), vault.VaultId));

            return token;
        }


        public virtual RestResponse MakeApiRequest(string secretArn, RestRequest request, string requestId = "", string correlationId = "" )
        {
            return MakeApiRequestAsync(secretArn, request, requestId, correlationId ).Result;
        }


        public async virtual Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = "", string correlationId = "")
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(ApiRestClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(request.Resource), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_Request_Resource_Is_NullOrEmpty);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executing, request.Method.ToString(), ApiBaseUri.ToString(), request.Resource));

            if (Token == null)
            {
                AwsSecretProvider provider = new AwsSecretProvider(Region.SystemName, AwsProfileName, ShouldUseLocalAwsProfile);
                AwsSecretVault vault = (AwsSecretVault)provider.GetSecretVaultByVaultId(secretArn, "AwsApiGatewayHelper.MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = \"\", string correlationId = \"\")");
                Token = await GetOAuthTokenUsingSecretCredentialsAsync(vault);
                // TODO save the token in the secret

            }
            request.AddHeader("authorization", "Bearer " + Token);

            // add the Correlation ID header, if it is a request transaction
            if(!string.IsNullOrEmpty(correlationId ))
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
                Logger.Info(string.Format("Request succeeded. Status code: {0}. CorrelationId: {1}", response.StatusCode,correlationId));
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
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executed, request.Method.ToString(), ApiBaseUri.ToString(), request.Resource, response.StatusCode));
            return response;
        }

    }
}

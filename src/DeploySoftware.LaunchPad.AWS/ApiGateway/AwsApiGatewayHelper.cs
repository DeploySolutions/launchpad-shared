using Abp.Dependency;
using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.SecretsManager;
using DeploySoftware.LaunchPad.Core.Api;
using DeploySoftware.LaunchPad.Core.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class AwsApiGatewayHelper : HelperBase, IAwsApiGatewayHelper
    {

        public AwsCommonHelper AwsCommonHelper { get; set; }

        public string OAuthBaseUrl { get; set; }
        public string OAuthTokenEndpoint { get; set; }

        public string ApiBaseUrl { get; set; }

        public string DefaultVersion { get; set; }

        [JsonIgnore]

        public RestClient OAuthClient { get; set; }
        [JsonIgnore]
        public RestClient ApiRestClient { get; set; }

        protected AwsSecretsManagerHelper _secretHelper;

        [JsonIgnore]
        public TemporaryAccessToken Token { get; set; }

        public AwsApiGatewayHelper() : base()
        {
            _secretHelper = new AwsSecretsManagerHelper();
            OAuthTokenEndpoint = string.Empty;
            OAuthBaseUrl = string.Empty;
            ApiBaseUrl = string.Empty;
            DefaultVersion = string.Empty;
        }

        public AwsApiGatewayHelper(string apiGatewayBaseUrl) : base()
        {
            _secretHelper = new AwsSecretsManagerHelper();
            OAuthTokenEndpoint = string.Empty;
            OAuthBaseUrl = string.Empty;
            ApiBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = string.Empty;
        }

        public AwsApiGatewayHelper(AwsSecretsManagerHelper secretHelper, string oAuthBaseUrl, string oAuthTokenEndpoint, string apiGatewayBaseUrl, string defaultApiVersion, ILogger logger) : base(logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint;
            ApiBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = defaultApiVersion;

            // set the REST clients
            OAuthClient = new RestClient(OAuthBaseUrl);
            ApiRestClient = new RestClient(apiGatewayBaseUrl);
        }

        public AwsApiGatewayHelper(AwsSecretsManagerHelper secretHelper, string awsRegionEndpointName, string oAuthBaseUrl, string oAuthTokenEndpoint, string apiGatewayBaseUrl, string defaultApiVersion, ILogger logger) : base(logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint;
            ApiBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = defaultApiVersion;

            // set the REST clients
            OAuthClient = new RestClient(OAuthBaseUrl);
            ApiRestClient = new RestClient(apiGatewayBaseUrl);
        }

        public virtual TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null)
        {
            return GetOAuthTokenUsingSecretCredentialsAsync(secretArn, scopes).Result;
        }

        /// <summary>
        /// Request and returns the OAuth token from the information which is stored in the given secret
        /// </summary>
        /// <param name="secretArn">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A TemporaryAccessToken object</returns>
        public async virtual Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentialsAsync(string secretArn, IList<string> scopes = null)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(OAuthClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            _logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Getting, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));
            TemporaryAccessToken token = null;

            string accessToken = string.Empty;

            string secretJson = await _secretHelper.GetJsonFromSecretAsync(secretArn);
            dynamic secret = JsonConvert.DeserializeObject(secretJson);

            // request the temporary token from the oAuth base url and the token endpoint
            var oAuthRequest = new RestRequest(Method.POST);
            oAuthRequest.Resource = OAuthTokenEndpoint;

            // set the headers including the authorization credentials from the secret
            StringBuilder sbGrant = new StringBuilder();
            sbGrant.Append("grant_type=client_credentials&client_id=");
            sbGrant.Append(secret.apiGatewayClientId);
            sbGrant.Append("&client_secret=");
            sbGrant.Append(secret.apiGatewayClientSecret);
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
            IRestResponse oAuthResponse = await OAuthClient.ExecuteAsync(oAuthRequest);
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

                _logger.Debug(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_AccessToken, token.TokenType, token.ExpiresIn, token.AccessToken));

            }
            _logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Got, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));

            return token;
        }


        public virtual IRestResponse MakeApiRequest(string secretArn, IRestRequest request, string requestId = "", string correlationId = "" )
        {
            return MakeApiRequestAsync(secretArn, request, requestId, correlationId ).Result;
        }


        public async virtual Task<IRestResponse> MakeApiRequestAsync(string secretArn, IRestRequest request, string requestId = "", string correlationId = "")
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(ApiRestClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(request.Resource), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_Request_Resource_Is_NullOrEmpty);
            _logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executing, request.Method.ToString(), ApiBaseUrl, request.Resource));

            if (Token == null)
            {
                Token = await GetOAuthTokenUsingSecretCredentialsAsync(secretArn);
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
            IRestResponse response = await ApiRestClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                _logger.Info(string.Format("Request succeeded. Status code: {0}. CorrelationId: {1}", response.StatusCode,correlationId));
            }
            else
            {
                _logger.Error(string.Format("Request failed with status code {0}. Reason: {1}. CorrelationId: {2}", 
                                response.StatusCode,
                                response.ErrorException.Message,
                                correlationId),
                        response.ErrorException)
                ;
            }
            _logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executed, request.Method.ToString(), ApiBaseUrl, request.Resource, response.StatusCode));
            return response;
        }


    }
}

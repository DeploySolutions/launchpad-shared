using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
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
    public partial class ApiGatewayHelper : AwsHelperBase
    {
        public string OAuthBaseUrl { get; set; }
        public string OAuthTokenEndpoint { get; set; }

        public string ApiGatewayBaseUrl { get; set; }

        public string DefaultVersion { get; set; }

        public RestClient OAuthClient { get; set; } 
        public RestClient ApiGatewayClient { get; set; }

        protected AwsSecretHelper _secretHelper;

        public TemporaryAccessToken Token { get; set; }

        protected ApiGatewayHelper() : base()
        {
            _secretHelper = new AwsSecretHelper(Logger);
            OAuthTokenEndpoint = string.Empty;
            OAuthBaseUrl = string.Empty;
            ApiGatewayBaseUrl = string.Empty;
            DefaultVersion = string.Empty;
        }

        public ApiGatewayHelper(string apiGatewayBaseUrl) : base()
        {
            _secretHelper = new AwsSecretHelper(Logger);
            OAuthTokenEndpoint = string.Empty;
            OAuthBaseUrl = string.Empty;
            ApiGatewayBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = string.Empty;
        }

        public ApiGatewayHelper(AwsSecretHelper secretHelper, string oAuthBaseUrl, string oAuthTokenEndpoint, string apiGatewayBaseUrl,string defaultApiVersion, ILogger logger) : base(logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint;
            ApiGatewayBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = defaultApiVersion;
            
            // set the REST clients
            OAuthClient = new RestClient(OAuthBaseUrl);
            ApiGatewayClient = new RestClient(apiGatewayBaseUrl);
        }

        public ApiGatewayHelper(AwsSecretHelper secretHelper, string awsRegionEndpointName, string oAuthBaseUrl, string oAuthTokenEndpoint, string apiGatewayBaseUrl, string defaultApiVersion, ILogger logger) : base(awsRegionEndpointName, logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint; 
            ApiGatewayBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = defaultApiVersion;

            // set the REST clients
            OAuthClient = new RestClient(OAuthBaseUrl);
            ApiGatewayClient = new RestClient(apiGatewayBaseUrl);
        }

        /// <summary>
        /// Request and returns the OAuth token from the information which is stored in the given secret
        /// </summary>
        /// <param name="secretArn">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A TemporaryAccessToken object</returns>
        public async virtual Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(OAuthClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Getting, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));
            TemporaryAccessToken token = null;

            string accessToken = string.Empty;

            string secretJson = await _secretHelper.GetJsonFromSecret(secretArn);
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
            if(scopes != null)
            {
                sbGrant.Append("&scope=");
                foreach(var scope in scopes)
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

                Logger.Debug(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_AccessToken, token.TokenType, token.ExpiresIn, token.AccessToken));

            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Got, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));
            
            return token;
        }

        
        public async virtual Task<IRestResponse> MakeApiGatewayRequest(string secretArn, IRestRequest request)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(ApiGatewayClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(request.Resource), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_Request_Resource_Is_NullOrEmpty);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executing, request.Method.ToString(), ApiGatewayBaseUrl, request.Resource));
            
            if(Token == null)
            {
                Token = await GetOAuthTokenUsingSecretCredentials(secretArn);
                // TODO save the token in the secret

            }
            request.AddHeader("authorization", "Bearer " + Token);
            
            // make the request to the API gateway
            IRestResponse response = await ApiGatewayClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Logger.Info("Request succeeded. Status code: " + response.StatusCode);
            }
            else
            {
                Logger.Error("Request failed with status code " + response.StatusCode + ". Reason: " + response.ErrorException.Message);
            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executed, request.Method.ToString(), ApiGatewayBaseUrl, request.Resource, response.StatusCode));
            return response;
        }


    }
}

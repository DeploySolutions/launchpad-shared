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
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class ApiGatewayHelper : AwsHelperBase
    {
        public string OAuthBaseUrl { get; set; }
        public string OAuthTokenEndpoint { get; set; }


        protected SecretHelper _secretHelper;

        public string TemporaryAccessToken { get; set; }

        protected ApiGatewayHelper() : base()
        {
            _secretHelper = new SecretHelper(Logger);
            OAuthTokenEndpoint = string.Empty;
            OAuthBaseUrl = string.Empty;
        }

        public ApiGatewayHelper(SecretHelper secretHelper, string oAuthBaseUrl, string oAuthTokenEndpoint, ILogger logger) : base(logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint;
        }

        public ApiGatewayHelper(SecretHelper secretHelper, string awsRegionEndpointName, string oAuthBaseUrl, string oAuthTokenEndpoint, ILogger logger) : base(awsRegionEndpointName, logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint;
        }

        /// <summary>
        /// Returns a valid database connection string which is stored in "dbConnectionString" key in a Secrets Manager secret.
        /// </summary>
        /// <param name="secretArn">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A SQL connection string</returns>
        public async virtual Task<string> GetOAuthTokenUsingSecretCredentials(string secretArn)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Secret_Is_NullOrEmpty);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Getting, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));
            
            string accessToken = string.Empty;

            string secretJson = await _secretHelper.GetJsonFromSecret(secretArn);
            dynamic secret = JsonConvert.DeserializeObject(secretJson);
            
            // request the temporary token from the oAuth base url and the token endpoint
            var oAuthClient = new RestClient(OAuthBaseUrl);
            var oAuthRequest = new RestRequest(Method.POST);
            oAuthRequest.Resource = OAuthTokenEndpoint;

            // set the headers including the authorization credentials from the secret
            string grantString = "grant_type=client_credentials&client_id=" + secret.catalystApiGatewayClientId + "&client_secret=" + secret.catalystApiGatewayClientSecret;            
            oAuthRequest.AddHeader("cache-control", "no-cache");
            oAuthRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            oAuthRequest.AddParameter("application/x-www-form-urlencoded", grantString, ParameterType.RequestBody);

            // request the token
            IRestResponse oAuthResponse = await oAuthClient.ExecuteAsync(oAuthRequest);
            if (oAuthResponse.IsSuccessful)
            {
               
                // read the content
                string oAuthResponseContent = oAuthResponse.Content;
                dynamic oAuthResponseJson = JsonConvert.DeserializeObject(oAuthResponseContent);
                accessToken = oAuthResponseJson.access_token;
                string expiresIn = oAuthResponseJson.expires_in;
                string tokenType = oAuthResponseJson.token_type;

                Logger.Debug(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_AccessToken, tokenType, expiresIn, accessToken));

            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Got, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));
            
            return accessToken;
        }

        
        public async virtual Task<IRestResponse> MakeApiGatewayRequest(string secretArn, string apiGatewayBaseUrl, string apiVersion, string apiGatewayEndpoint, IRestRequest request)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetDbConnectionStringFromSecret_Getting, apiGatewayBaseUrl));
            
            if(string.IsNullOrEmpty(TemporaryAccessToken))
            {
                TemporaryAccessToken = await GetOAuthTokenUsingSecretCredentials(secretArn);
                // TODO save the token in the secret

            }
            var apiClient = new RestClient(apiGatewayBaseUrl);
            request.Resource = apiVersion + "/" + apiGatewayEndpoint;
            request.AddHeader("authorization", "Bearer " + TemporaryAccessToken);
            
            // make the request to the API gateway
            IRestResponse response = await apiClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Logger.Info("Request succeeded. Status code: " + response.StatusCode);
            }
            else
            {
                Logger.Error("Request failed with status code " + response.StatusCode + ". Reason: " + response.ErrorException.Message);
            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetDbConnectionStringFromSecret_Got, TemporaryAccessToken));
            return response;
        }


    }
}

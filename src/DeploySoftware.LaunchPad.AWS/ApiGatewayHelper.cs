﻿using Amazon;
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

        protected RestClient _oAuthClient; 
        protected RestClient _apiClient;

        protected SecretHelper _secretHelper;

        public string TemporaryAccessToken { get; set; }

        protected ApiGatewayHelper() : base()
        {
            _secretHelper = new SecretHelper(Logger);
            OAuthTokenEndpoint = string.Empty;
            OAuthBaseUrl = string.Empty;
            ApiGatewayBaseUrl = string.Empty;
            DefaultVersion = string.Empty;
        }

        public ApiGatewayHelper(SecretHelper secretHelper, string oAuthBaseUrl, string oAuthTokenEndpoint, string apiGatewayBaseUrl,string defaultApiVersion, ILogger logger) : base(logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint;
            ApiGatewayBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = defaultApiVersion;
            
            // set the REST clients
            _oAuthClient = new RestClient(OAuthBaseUrl);
            _apiClient = new RestClient(apiGatewayBaseUrl);
        }

        public ApiGatewayHelper(SecretHelper secretHelper, string awsRegionEndpointName, string oAuthBaseUrl, string oAuthTokenEndpoint, string apiGatewayBaseUrl, string defaultApiVersion, ILogger logger) : base(awsRegionEndpointName, logger)
        {
            _secretHelper = secretHelper;
            OAuthBaseUrl = oAuthBaseUrl;
            OAuthTokenEndpoint = oAuthTokenEndpoint; 
            ApiGatewayBaseUrl = apiGatewayBaseUrl;
            DefaultVersion = defaultApiVersion;

            // set the REST clients
            _oAuthClient = new RestClient(OAuthBaseUrl);
            _apiClient = new RestClient(apiGatewayBaseUrl);
        }

        /// <summary>
        /// Returns a valid database connection string which is stored in "dbConnectionString" key in a Secrets Manager secret.
        /// </summary>
        /// <param name="secretArn">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A SQL connection string</returns>
        public async virtual Task<string> GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(_oAuthClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_GetOAuthTokenUsingSecretCredentials_Getting, OAuthTokenEndpoint, OAuthBaseUrl, secretArn));
            
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
            IRestResponse oAuthResponse = await _oAuthClient.ExecuteAsync(oAuthRequest);
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

        
        public async virtual Task<IRestResponse> MakeApiGatewayRequest(string secretArn, IRestRequest request)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(secretArn), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_SecretArn_Is_NullOrEmpty);
            Guard.Against<ArgumentNullException>(_apiClient == null, DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_RestClient_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(request.Resource), DeploySoftware_LaunchPad_AWS_Resources.ApiGatewayHelper_MakeApiGatewayRequest_Request_Resource_Is_NullOrEmpty);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_ExecuteApiGatewayRequest_Executing, request.Method.ToString(), ApiGatewayBaseUrl, request.Resource));
            
            if(string.IsNullOrEmpty(TemporaryAccessToken))
            {
                TemporaryAccessToken = await GetOAuthTokenUsingSecretCredentials(secretArn);
                // TODO save the token in the secret

            }
            request.AddHeader("authorization", "Bearer " + TemporaryAccessToken);
            
            // make the request to the API gateway
            IRestResponse response = await _apiClient.ExecuteAsync(request);
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
﻿using DeploySoftware.LaunchPad.Core.Api;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public interface IAwsApiGatewayHelper : IAwsHelper
    {
        string ApiBaseUrl { get; set; }
        RestClient ApiRestClient { get; set; }
        string DefaultVersion { get; set; }
        string OAuthBaseUrl { get; set; }
        RestClient OAuthClient { get; set; }
        string OAuthTokenEndpoint { get; set; }
        TemporaryAccessToken Token { get; set; }

        TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null);
        Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentialsAsync(string secretArn, IList<string> scopes = null);
        RestResponse MakeApiRequest(string secretArn, RestRequest request, string requestId = "", string correlationId = "");
        Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = "", string correlationId = "");
    }
}
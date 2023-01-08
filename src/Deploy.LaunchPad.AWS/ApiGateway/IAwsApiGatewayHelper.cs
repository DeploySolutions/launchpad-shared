using Amazon.APIGateway;
using Deploy.LaunchPad.Core.Api;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS
{
    public interface IAwsApiGatewayHelper : IAwsHelper<AmazonAPIGatewayConfig>
    {
        Uri ApiBaseUri { get; set; }
        RestClient ApiRestClient { get; set; }
        string DefaultVersion { get; set; }
        Uri OAuthBaseUri { get; set; }
        RestClient OAuthClient { get; set; }
        string OAuthTokenEndpoint { get; set; }
        TemporaryAccessToken Token { get; set; }

        public TemporaryAccessToken GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null);

        public Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentialsAsync(AwsSecretVault vault, IList<string> scopes = null);

        RestResponse MakeApiRequest(string secretArn, RestRequest request, string requestId = "", string correlationId = "");
        Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string requestId = "", string correlationId = "");
    }
}
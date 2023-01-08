using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Api
{
    public interface IApiHelper
    {
        string ApiBaseUrl { get; set; }
        RestClient ApiRestClient { get; set; }
        string DefaultVersion { get; set; }
        string OAuthBaseUrl { get; set; }
        RestClient OAuthClient { get; set; }
        string OAuthTokenEndpoint { get; set; }
        TemporaryAccessToken Token { get; set; }

        Task<TemporaryAccessToken> GetOAuthTokenUsingSecretCredentials(string secretArn, IList<string> scopes = null);
        RestResponse MakeApiRequest(string secretArn, RestRequest request, string correlationId = "", string causationId = "");

        Task<RestResponse> MakeApiRequestAsync(string secretArn, RestRequest request, string correlationId = "", string causationId = "");

    }
}
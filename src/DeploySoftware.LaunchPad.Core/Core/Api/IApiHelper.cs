using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Api
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
        IRestResponse MakeApiRequest(string secretArn, IRestRequest request, string correlationId = "", string causationId = "");

        Task<IRestResponse> MakeApiRequestAsync(string secretArn, IRestRequest request, string correlationId = "", string causationId = "");
        
    }
}
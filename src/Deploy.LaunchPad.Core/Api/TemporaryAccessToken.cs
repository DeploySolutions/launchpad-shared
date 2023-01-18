using System;
using System.Text.Json.Serialization;

namespace Deploy.LaunchPad.Core.Api
{
    public class TemporaryAccessToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";

        public int ExpiresIn { get; set; }

        public DateTime ExpiryDateTime { get; set; }

        public TemporaryAccessToken()
        {

        }

    }
}

using System;

namespace Deploy.LaunchPad.Core.Api
{
    public class TemporaryAccessToken
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public DateTime ExpiryDateTime { get; set; }

        public string TokenType { get; set; } = "Bearer";

    }
}

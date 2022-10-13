using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Api
{
    public class TemporaryAccessToken
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public DateTime ExpiryDateTime { get; set; }

        public string TokenType { get; set; } = "Bearer";

    }
}

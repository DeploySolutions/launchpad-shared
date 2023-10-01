using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.WebApp
{
    [Serializable]
    public partial class CorsSettings
    {
        public virtual IDictionary<string, string> AllowedHosts { get; set; }

        public virtual IDictionary<string, string> AllowedMethods { get; set; }

        public virtual IDictionary<string, string> AccessControlAllowHeaders { get; set; }

        public virtual bool AccessControlAllowCredentials { get; set; } = false;


        public CorsSettings()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            AllowedHosts = new Dictionary<string, string>(comparer)
            {
                { "[PUT SOMETHING HERE, DO NOT USE * ]", "[PUT SOMETHING HERE, DO NOT USE * ]" }
            };
            

            AllowedMethods = new Dictionary<string, string>(comparer)
            {
                { "GET", "GET" },
                { "OPTIONS", "OPTIONS" }
            };

            AccessControlAllowHeaders = new Dictionary<string, string>(comparer)
            {
                { "Accept", "Accept" }
            };

        }
    }
}

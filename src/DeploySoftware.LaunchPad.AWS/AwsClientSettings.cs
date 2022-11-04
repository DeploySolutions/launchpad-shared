using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    [Serializable]
    public partial class AwsClientSettings<TClientConfig> : IAwsClientSettings<TClientConfig>
        where TClientConfig : ClientConfig, new()
    {
        public virtual TClientConfig Config { get; set; }
        public AwsClientSettings()
        {
            Config = new TClientConfig();
        }

        public AwsClientSettings(TClientConfig config)
        {
            Config = config;
        }
    }
}

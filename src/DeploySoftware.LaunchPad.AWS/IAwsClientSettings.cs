using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public interface IAwsClientSettings<TClientConfig>
        where TClientConfig : ClientConfig, new()
    {
        public TClientConfig Config { get; set; }
        
    }
}

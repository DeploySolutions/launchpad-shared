using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    [Serializable]
    public partial class AwsClientSettings : IAwsClientSettings
    {
        public virtual int RequestTimeoutInMs { get; set; } = 10000;
        public virtual int SocketReadWriteTimeoutInMs { get; set; } = 30000;
        public virtual int MaxErrorRetry { get; set; } = 2;

        public AwsClientSettings()
        {

        }
    }
}

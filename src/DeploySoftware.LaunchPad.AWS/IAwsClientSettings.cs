using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public interface IAwsClientSettings
    {
        public int RequestTimeoutInMs { get; set; }

        public int SocketReadWriteTimeoutInMs { get; set; }

        public int MaxErrorRetry { get; set; }

    }
}

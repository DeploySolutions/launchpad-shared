using Amazon;
using Amazon.Runtime;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public interface IAwsHelper : IHelper
    {
        public RegionEndpoint Region { get; set; }

        public AWSCredentials GetAwsCredentialsFromNamedLocalProfile(string awsProfileName);

        public RegionEndpoint GetRegionEndpoint(string awsRegionEndpointSystemName);

    }
}

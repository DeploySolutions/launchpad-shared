using Deploy.LaunchPad.Infra.Aws;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Infra.AWS.Application.Configuration
{
    public partial class AwsCloudConfiguration
    {
        public virtual string RegionEndpointName { get; set; } = Constants_LaunchPadInfraAWS.DefaultRegionEndpointName;

        public virtual AwsCloudConfigurationCredentials Credentials { get; set; } 

        public AwsCloudConfiguration() 
        {
            Credentials = new AwsCloudConfigurationCredentials();   
        }
    }

    public partial class AwsCloudConfigurationCredentials
    {
       
        public virtual string LocalProfileName { get; set; } = Constants_LaunchPadInfraAWS.DefaultLocalProfileName;

        public virtual bool ShouldUseLocalProfile { get; set; } = false;

        public AwsCloudConfigurationCredentials() { }
    }
}

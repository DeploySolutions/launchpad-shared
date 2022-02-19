using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public class SolutionInfrastructure : ISolutionInfrastructure
    {

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual CloudProviderEnum CloudProvider { get; set; }

        public virtual AbpFrameworkEnum AbpFramework { get; set; }

        public virtual bool SearchIsEnabled { get; set; }

        public SolutionInfrastructure()
        {
            CloudProvider = CloudProviderEnum.AmazonWebServices;
            AbpFramework = AbpFrameworkEnum.AspNetBoilerplate;
            Name = CloudProvider.ToString();
            Description = string.Empty;
        }

        public SolutionInfrastructure(CloudProviderEnum cloudProvider, AbpFrameworkEnum abpFramework)
        {
            CloudProvider = cloudProvider;
            AbpFramework = abpFramework;
            Name = CloudProvider.ToString();
            Description = string.Empty;
        }

    }
}

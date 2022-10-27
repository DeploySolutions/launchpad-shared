using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class SoftwareInfrastructure : ISoftwareInfrastructure
    {

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual CloudProviderEnum CloudProvider { get; set; } = CloudProviderEnum.AWS;

        public virtual AbpFrameworkEnum AbpFramework { get; set; } = AbpFrameworkEnum.Abp;

        public virtual InfrastructureAsCodeFrameworkEnum InfrastructureAsCodeFramework { get; set; } = InfrastructureAsCodeFrameworkEnum.Terraform;

        public virtual bool SearchIsEnabled { get; set; }

        public bool ShouldOverrideParent { get; set; } = false;

        public SoftwareInfrastructure()
        {
            Name = AbpFramework.ToString() + "." + CloudProvider.ToString();
            Description = string.Format(
                "This element will use the coding framework '{0}' and will deploy in '{1}'.",
                AbpFramework.ToString(),
                CloudProvider.ToString()
            );
        }

        public SoftwareInfrastructure(CloudProviderEnum cloudProvider, AbpFrameworkEnum abpFramework)
        {
            CloudProvider = cloudProvider;
            AbpFramework = abpFramework;
            Name = AbpFramework.ToString() + "." + CloudProvider.ToString();
            Description = string.Format(
                "This element will use the coding framework '{0}' and will deploy in '{1}'.",
                AbpFramework.ToString(),
                CloudProvider.ToString()
            );
        }

    }
}

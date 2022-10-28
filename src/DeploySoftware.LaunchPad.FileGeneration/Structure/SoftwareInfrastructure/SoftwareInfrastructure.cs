using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class SoftwareInfrastructure : ISoftwareInfrastructure
    {

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual CloudProviderEnum CloudProvider { get; set; } = CloudProviderEnum.AWS;

        public virtual AbpFrameworkEnum AbpFramework { get; set; } = AbpFrameworkEnum.Abp;

        public virtual IDictionary<string, IInfrastructureAsCodeFramework> InfrastructureAsCodeFrameworks { get; set; }

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
            var comparer = StringComparer.OrdinalIgnoreCase;
            InfrastructureAsCodeFrameworks = new Dictionary<string, IInfrastructureAsCodeFramework>(comparer);
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
            var comparer = StringComparer.OrdinalIgnoreCase;
            InfrastructureAsCodeFrameworks = new Dictionary<string, IInfrastructureAsCodeFramework>(comparer);
        }


        public virtual string GetSolutionFolderName()
        {
            return AbpFramework.ToString() + "." + CloudProvider.ToString();
        }

    }
}

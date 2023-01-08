using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ISoftwareInfrastructure
    {
        public CloudProviderEnum CloudProvider { get; set; }

        public AbpFrameworkEnum AbpFramework { get; set; }

        public IDictionary<string, IInfrastructureAsCodeFramework> InfrastructureAsCodeFrameworks { get; set; }

        public bool SearchIsEnabled { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public bool ShouldOverrideParent { get; set; }

        public string GetSolutionFolderName();

    }
}
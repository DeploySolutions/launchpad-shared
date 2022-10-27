﻿namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface ISoftwareInfrastructure
    {
        public CloudProviderEnum CloudProvider { get; set; }

        public AbpFrameworkEnum AbpFramework { get; set; }

        public InfrastructureAsCodeFrameworkEnum InfrastructureAsCodeFramework { get; set; }

        public bool SearchIsEnabled { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public bool ShouldOverrideParent { get; set; }

    }
}
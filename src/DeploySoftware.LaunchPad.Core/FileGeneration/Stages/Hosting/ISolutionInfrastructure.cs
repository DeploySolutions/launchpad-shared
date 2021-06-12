﻿namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ISolutionInfrastructure
    {
        public SupportedCloudProviderEnum CloudProvider { get; set; }

        public AbpFrameworkEnum AbpFramework { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ISolutionInfrastructure
    {
        public CloudProviderEnum CloudProvider { get; set; }

        public AbpFrameworkEnum AbpFramework { get; set; }

        public bool SearchIsEnabled { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface IInfrastructureAsCodeFramework
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public InfrastructureAsCodeFrameworkTypeEnum Type { get; set; }


    }
}

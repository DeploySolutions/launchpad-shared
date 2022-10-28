namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface IWebAppSolution
    {
        public ISoftwareInfrastructure SoftwareInfrastructure { get; set; }
        public WebAppModule WebAppModule { get; set; }

        bool CheckValidity();
    }
}
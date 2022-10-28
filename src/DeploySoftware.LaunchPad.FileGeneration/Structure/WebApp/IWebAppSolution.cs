namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface IWebAppSolution : IHaveSoftwareInfrastructure
    {
        public WebAppModule WebAppModule { get; set; }

        bool CheckValidity();
    }
}
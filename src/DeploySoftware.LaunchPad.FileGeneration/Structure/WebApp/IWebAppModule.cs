using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface IWebAppModule
    {
        public ISoftwareInfrastructure SoftwareInfrastructure { get; set; }
        public VisualStudioComponent WebApi { get; set; }
        public IDictionary<string, WebClientComponent> WebClients { get; set; }
    }
}
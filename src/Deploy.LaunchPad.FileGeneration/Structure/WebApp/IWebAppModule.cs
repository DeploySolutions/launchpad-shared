using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface IWebAppModule : IHaveSoftwareInfrastructure
    {
        public VisualStudioComponent WebApi { get; set; }
        public IDictionary<string, WebClientComponent> WebClients { get; set; }
    }
}
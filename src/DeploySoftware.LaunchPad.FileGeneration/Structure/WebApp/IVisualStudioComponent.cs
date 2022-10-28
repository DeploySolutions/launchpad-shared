using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public partial interface IVisualStudioComponent
    {
        public IDictionary<string, LaunchPadGeneratedApplicationService> ApplicationServices { get; set; }
        public IDictionary<string, LaunchPadGeneratedDomainEntity> DomainEntities { get; set; }
        public ISoftwareInfrastructure SoftwareInfrastructure { get; set; }

        bool CheckValidity();
    }
}
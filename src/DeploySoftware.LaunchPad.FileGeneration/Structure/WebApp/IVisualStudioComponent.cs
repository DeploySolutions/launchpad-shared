using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public partial interface IVisualStudioComponent : IHaveSoftwareInfrastructure
    {
        public IDictionary<string, LaunchPadGeneratedApplicationService> ApplicationServices { get; set; }
        public IDictionary<string, LaunchPadGeneratedDomainEntity> DomainEntities { get; set; }

        bool CheckValidity();
    }
}
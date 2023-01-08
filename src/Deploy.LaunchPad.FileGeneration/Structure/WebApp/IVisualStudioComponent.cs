using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface IVisualStudioComponent : IHaveSoftwareInfrastructure
    {
        public IDictionary<string, LaunchPadGeneratedApplicationService> ApplicationServices { get; set; }
        public IDictionary<string, LaunchPadGeneratedDomainEntity> DomainEntities { get; set; }

        public IDictionary<string, LaunchPadGeneratedValueObject> ValueObjects { get; set; }

        bool CheckValidity();
    }
}
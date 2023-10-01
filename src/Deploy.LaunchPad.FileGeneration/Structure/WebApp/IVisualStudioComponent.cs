using System.Collections.Generic;
using Deploy.LaunchPad.FileGeneration.Structure.WebApp;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface IVisualStudioComponent : IHaveSoftwareInfrastructure
    {
        public CorsSettings Cors { get; set; }
        public IDictionary<string, LaunchPadGeneratedApplicationService> ApplicationServices { get; set; }
        public IDictionary<string, LaunchPadGeneratedDomainEntity> DomainEntities { get; set; }

        public IDictionary<string, LaunchPadGeneratedValueObject> ValueObjects { get; set; }

        bool CheckValidity();
    }
}
using Deploy.LaunchPad.Core.Abp.Domain;
using Deploy.LaunchPad.Core.Domain;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    public partial interface IEarthObservationSwath<TPrimaryKey, TFileStorageLocationType> : IDomainEntity<TPrimaryKey>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public IDictionary<TPrimaryKey, IEarthObservationScene<TPrimaryKey, TFileStorageLocationType>> Scenes { get; set; }
    }
}

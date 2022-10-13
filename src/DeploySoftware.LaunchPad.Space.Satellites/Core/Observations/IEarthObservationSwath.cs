using DeploySoftware.LaunchPad.Core.Abp.Domain;
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Core.Observations
{
    public partial interface IEarthObservationSwath<TPrimaryKey, TFileStorageLocationType> : IDomainEntity<TPrimaryKey>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public IDictionary<TPrimaryKey, IEarthObservationScene<TPrimaryKey, TFileStorageLocationType>> Scenes { get; set; }
    }
}

using DeploySoftware.LaunchPad.Core.Domain;
using DeploySoftware.LaunchPad.Space.Satellites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Landsat
{
    public partial class Landsat8Observation<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>, 
        ILandsatObservation<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public string Bucket { get; set; }
        public string ProductId { get; set; }
        public string Collection { get; set; }
        public string Projection { get; set; }
        public string SensorName { get; set; }
        public string YearAcquired { get; set; }
        public string Path { get; set; }
        public string Row { get; set; }
        

        public Landsat8Observation()
        {
            
        }

        public int CompareTo(DomainEntityBase<TPrimaryKey> other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(DomainEntityBase<TPrimaryKey> other)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}

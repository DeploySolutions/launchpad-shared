﻿using DeploySoftware.LaunchPad.Core.Domain;
using DeploySoftware.LaunchPad.Space.Satellites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Landsat
{
    public partial class Sentinel1Observation<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>, 
        ISentinel1Observation<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public string Bucket { get; set; }
        public string ProductId { get; set; }
        public string Path { get; set; }
        public string ProductType { get; set; }
        public string Version { get; set; }

        public Sentinel1Observation()
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

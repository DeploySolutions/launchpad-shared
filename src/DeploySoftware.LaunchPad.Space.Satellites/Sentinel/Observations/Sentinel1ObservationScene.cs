﻿using DeploySoftware.LaunchPad.Core.Abp.Domain;
using DeploySoftware.LaunchPad.Core.Domain;
using DeploySoftware.LaunchPad.Space.Satellites.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Sentinel
{
    public partial class Sentinel1ObservationScene<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>, 
        ISentinel1ObservationScene<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public string Bucket { get; set; }
        public string ProductId { get; set; }
        public string Path { get; set; }
        public string ProductType { get; set; }
        public string Version { get; set; }

        public Sentinel1ObservationScene()
        {
            
        }

    }
}

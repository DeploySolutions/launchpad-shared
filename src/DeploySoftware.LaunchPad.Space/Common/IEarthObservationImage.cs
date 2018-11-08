using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using DeploySoftware.LaunchPad.Shared.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{
    public interface IEarthObservationImage<TPrimaryKey> : IEntity<TPrimaryKey>, IHasCreationTime, IHasModificationTime
    {
        [Required]
        GeographicLocation SceneCentre { get; set; }

        [Required]
        List<GeographicLocation> CornerCoordinates { get; set; }

        [Required]
        string TIFImagePath { get; set; }

        [Required]
        string Name { get; set; }
        
        string Description { get; set; }

    }
}

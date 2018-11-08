using DeploySoftware.LaunchPad.Shared.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{
    public abstract class EarthObservationImageBase<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IEarthObservationImage<TPrimaryKey>
    {
        public const int MaxNameLength = 4 * 1024; //4KB
        public const int MaxDescriptionLength = 4 * 1024; //4KB
        
        [Required]
        public virtual GeographicLocation SceneCentre { get; set; }

        [Required]
        public virtual List<GeographicLocation> CornerCoordinates { get; set; }

        [Required]
        public virtual string TIFImagePath { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }
    }
}

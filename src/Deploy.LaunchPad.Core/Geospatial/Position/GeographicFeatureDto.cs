using Deploy.LaunchPad.Core.Geospatial.ReferencePoint;
using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    public partial class GeographicFeatureDto : GeographicPositionDto, IAmGeographicFeature
    {
        public virtual string FeatureId { get; set; } = Guid.NewGuid().ToString();

        public virtual ElementName Name { get; set; } = new ElementName(string.Empty);

        public virtual ElementDescription Description { get; set; } = new ElementDescription(string.Empty);
        
        public virtual Area? Area { get; set; }

        public virtual Height? Height { get; set; }
        protected GeographicFeatureDto() :base()
        {
        }

        public GeographicFeatureDto(string geoJson, string key) : base(geoJson)
        {
            FeatureId = key;
        }
    }
}

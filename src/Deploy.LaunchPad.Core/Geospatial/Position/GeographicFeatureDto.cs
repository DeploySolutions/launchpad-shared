using System;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    public partial class GeographicFeatureDto : GeographicPositionDto, IAmGeographicFeature
    {
        public virtual string Key { get; set; } = Guid.NewGuid().ToString();

        public virtual ElementName Name { get; set; } = new ElementName(string.Empty);

        public virtual ElementDescription Description { get; set; } = new ElementDescription(string.Empty);

        protected GeographicFeatureDto() :base()
        {
        }

        public GeographicFeatureDto(string geoJson, string key) : base(geoJson)
        {
            Key = key;
        }
    }
}

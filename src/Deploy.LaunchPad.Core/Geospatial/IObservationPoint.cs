using Deploy.LaunchPad.Core.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Geospatial.H3;
using NetTopologySuite.Geometries;

namespace Deploy.LaunchPad.Core.Geospatial
{
    public partial interface IObservationPoint<TPrimaryKey, TParentAreaOfInterest> : IHaveGeographicPosition, IHaveGeoJsonDefinition, IHaveH3Definition, IHaveElevation
        where TParentAreaOfInterest : IAreaOfInterest<TPrimaryKey>
    {
        public TParentAreaOfInterest ParentAoi { get; set; }

    }
}

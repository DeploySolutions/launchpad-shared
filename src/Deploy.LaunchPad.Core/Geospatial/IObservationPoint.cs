using Deploy.LaunchPad.Core.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Geospatial.H3;
using NetTopologySuite.Geometries;

namespace Deploy.LaunchPad.Core.Geospatial
{
    public partial interface IObservationPoint<TParentAreaOfInterest> : IHaveGeographicPosition
        where TParentAreaOfInterest : IAreaOfInterest
    {
        public TParentAreaOfInterest ParentAoi { get; set; }

    }
}

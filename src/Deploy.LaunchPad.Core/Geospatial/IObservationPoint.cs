using Deploy.LaunchPad.Core.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Geospatial.H3;
using NetTopologySuite.Geometries;

namespace Deploy.LaunchPad.Core.Geospatial
{
    public partial interface IObservationPoint<TPrimaryKey, TGeoJsonType, TParentAreaOfInterest> : ICanBeDescribedInGeoJson<TGeoJsonType>, ICanBeDescribedInH3, IHaveElevation
        where TParentAreaOfInterest : Geometry
        where TGeoJsonType : Geometry
    {
        public TParentAreaOfInterest ParentAoi { get; set; }

    }
}

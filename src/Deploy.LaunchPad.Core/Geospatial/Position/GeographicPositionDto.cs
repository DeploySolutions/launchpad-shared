using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    [Serializable]
    public partial class GeographicPositionDto
    {
        public Geometry Geometry { get; set; }

        public string GeoJson { get; set; }

        public double[] UserDefinedBoundingBox { get; set;  }

        public Coordinate UserDefinedCenter { get; set; }

        public double? Elevation { get; set; }

        public GeographicPositionDto(string geoJson)
        {
            GeoJson = geoJson;
            GeospatialHelper helper = new GeospatialHelper();
            var geometry = helper.ConvertGeoJsonToGeometry(GeoJson);
            Geometry = geometry;
        }
        public GeographicPositionDto(string geoJson, double? elevation)
        {
            GeoJson = geoJson;
            Elevation = elevation;
            GeospatialHelper helper = new GeospatialHelper();
            var geometry = helper.ConvertGeoJsonToGeometry(GeoJson);
            Geometry = geometry;
        }
        public GeographicPositionDto(string geoJson, double? elevation, double[] userDefinedBoundingBox, Coordinate userDefinedCenter)
        {
            GeoJson = geoJson;
            Elevation = elevation;
            GeospatialHelper helper = new GeospatialHelper();
            var geometry = helper.ConvertGeoJsonToGeometry(GeoJson);
            Geometry = geometry;
            UserDefinedBoundingBox = userDefinedBoundingBox;
            UserDefinedCenter = userDefinedCenter;
        }

        public GeographicPositionDto(Geometry geometry, double? elevation, double[] userDefinedBoundingBox, Coordinate userDefinedCenter)
        {
            Geometry = geometry;
            GeospatialHelper helper = new GeospatialHelper();
            GeoJson = helper.ConvertGeometryToGeoJson(geometry);
            UserDefinedBoundingBox = userDefinedBoundingBox;
            UserDefinedCenter = userDefinedCenter;
            Elevation = elevation;
        }

    }
}

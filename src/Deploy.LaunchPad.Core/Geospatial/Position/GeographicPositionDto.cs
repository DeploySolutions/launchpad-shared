using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    public partial class GeographicPositionDto : IMustHaveGeographicPosition
    {
        // IMustHaveGeographicPosition
        public virtual bool IsPoint { get; set; }
        public virtual bool IsArea { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Latitude { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Longitude { get; set; }


        public virtual double CenterLatitude { get; set; }
        public virtual double CenterLongitude { get; set; }

        public virtual double RepresentativeLatitude { get; set; }
        public virtual double RepresentativeLongitude { get; set; }
        public virtual double CentroidLatitude { get; set; }
        public virtual double CentroidLongitude { get; set; }


        // IMayHaveBoundingBox
        public virtual double[]? BoundingBox { get; set; }

        // IMayHaveGeoJsonDefinition
        public virtual string? GeoJson { get; set; }

        // IMayHaveElevation
        public virtual double? Elevation { get; set; }

        // IMustHaveCountryId
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Guid CountryId { get; set; }

        public virtual Geometry Geometry { get; set; }

        public virtual Coordinate UserDefinedCenter { get; set; }

        protected GeographicPositionDto()
        {
        }

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
            if (geometry != null)
            {
                IsPoint = geometry is Point;
                IsArea = geometry is Polygon || geometry is MultiPolygon;
                Latitude = geometry.Centroid.Y;
                Longitude = geometry.Centroid.X;
                CenterLatitude = geometry.Centroid.Y;
                CenterLongitude = geometry.Centroid.X;
                RepresentativeLongitude = helper.GetRepresentativeCoordinate(geometry, new Coordinate(CenterLongitude, CentroidLatitude)).X;
                RepresentativeLatitude = helper.GetRepresentativeCoordinate(geometry, new Coordinate(CenterLongitude, CentroidLatitude)).Y;
                CentroidLatitude = geometry.Centroid.Y;
                CentroidLongitude = geometry.Centroid.X;
            }
        }
        public GeographicPositionDto(string geoJson, double? elevation, double[] userDefinedBoundingBox, Coordinate userDefinedCenter)
        {
            GeoJson = geoJson;
            Elevation = elevation;
            GeospatialHelper helper = new GeospatialHelper();
            var geometry = helper.ConvertGeoJsonToGeometry(GeoJson);
            Geometry = geometry;
            BoundingBox = userDefinedBoundingBox;
            UserDefinedCenter = userDefinedCenter;
            if (geometry != null)
            {
                IsPoint = geometry is Point;
                IsArea = geometry is Polygon || geometry is MultiPolygon;
                Latitude = geometry.Centroid.Y;
                Longitude = geometry.Centroid.X;
                CenterLatitude = geometry.Centroid.Y;
                CenterLongitude = geometry.Centroid.X;
                RepresentativeLongitude = helper.GetRepresentativeCoordinate(geometry, userDefinedCenter).X;
                RepresentativeLatitude = helper.GetRepresentativeCoordinate(geometry, userDefinedCenter).Y;
                CentroidLatitude = geometry.Centroid.Y;
                CentroidLongitude = geometry.Centroid.X;
            }
        }

        public GeographicPositionDto(Geometry geometry, double? elevation, double[] userDefinedBoundingBox, Coordinate userDefinedCenter)
        {
            Geometry = geometry;
            GeospatialHelper helper = new GeospatialHelper();
            GeoJson = helper.ConvertGeometryToGeoJson(geometry);
            BoundingBox = userDefinedBoundingBox;
            UserDefinedCenter = userDefinedCenter;
            Elevation = elevation;
            if (geometry != null)
            {
                IsPoint = geometry is Point;
                IsArea = geometry is Polygon || geometry is MultiPolygon;
                Latitude = geometry.Centroid.Y;
                Longitude = geometry.Centroid.X;
                CenterLatitude = geometry.Centroid.Y;
                CenterLongitude = geometry.Centroid.X;
                RepresentativeLongitude = helper.GetRepresentativeCoordinate(geometry, userDefinedCenter).X;
                RepresentativeLatitude = helper.GetRepresentativeCoordinate(geometry, userDefinedCenter).Y;
                CentroidLatitude = geometry.Centroid.Y;
                CentroidLongitude = geometry.Centroid.X;
            }
        }

    }
}

using Castle.Core.Logging;
using Deploy.LaunchPad.Util;
using H3.Extensions;
using H3;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Deploy.LaunchPad.Core.Geospatial.Position;

namespace Deploy.LaunchPad.Core.Geospatial
{
    public partial class GeospatialHelper : HelperBase
    {

        public GeospatialHelper() : base()
        {
        }

        /// <summary>
        /// Sets the geometry.
        /// </summary>
        /// <returns>Point.</returns>
        public virtual Geometry ConvertGeoJsonToGeometry(string geoJson)
        {
            var serializer = GeoJsonSerializer.Create();
            Geometry geom = null;
            try
            {
                using (var stringReader = new StringReader(geoJson))
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    geom = serializer.Deserialize<Point>(jsonReader);
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
            return geom;
        }

        /// <summary>
        /// Converts a Geometry object into a GeoJSON string.
        /// </summary>
        /// <param name="geometry">The Geometry object to convert.</param>
        /// <returns>A GeoJSON string representation of the Geometry object.</returns>
        public virtual string ConvertGeometryToGeoJson(Geometry geometry)
        {
            if (geometry == null)
            {
                throw new ArgumentNullException(nameof(geometry), "Geometry must not be null.");
            }
            try
            {
                var geoJsonWriter = new GeoJsonWriter();
                return geoJsonWriter.Write(geometry);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error converting Geometry to GeoJSON: {ex.Message}", ex);
                throw;
            }
        }



        /// <summary>
        /// Returns the representative point used for center calculations.
        /// Priority: user-defined center > Point coordinates > representative point of area.
        /// </summary>
        public virtual Coordinate? GetRepresentativeCoordinate(Geometry geometry, Coordinate _userDefinedCenter = null)
        {
            if (_userDefinedCenter != null)
                return _userDefinedCenter;

            if (geometry is Point pt)
                return pt.Coordinate;

            return geometry?.PointOnSurface?.Coordinate;
        }

        public virtual Coordinate GetCentroid(Geometry geometry)
        {
            return geometry.Centroid.Coordinate;
        }

        /// <summary>
        /// Determines if provided Point lat and lon are valid WGS84 coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="shouldThrowIfInvalid"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual bool ValidateWgs84Point(Point point, bool shouldThrowIfInvalid = false)
        {
            bool isValid = point.IsValid;
            var lat = point.Y;
            var lon = point.X;

            if (lat < -90)
            {
                isValid = false;
                if (shouldThrowIfInvalid)
                    throw new ArgumentOutOfRangeException(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90);
            }
            else if (lat > 90)
            {
                isValid = false;
                if (shouldThrowIfInvalid)
                    throw new ArgumentOutOfRangeException(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_GreaterThan_90);
            }
            else if (lon < -180)
            {
                isValid = false;
                if (shouldThrowIfInvalid)
                    throw new ArgumentOutOfRangeException(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_LessThan_Minus180);
            }
            else if (lon > 180)
            {
                isValid = false;
                if (shouldThrowIfInvalid)
                    throw new ArgumentOutOfRangeException(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_GreaterThan_180);
            }
            if (!isValid && shouldThrowIfInvalid)
                throw new ArgumentException(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Validate_Point);
            return isValid;
        }

        /// <summary>
        /// Gets the geographic position DTO representation of the item, as well as any user provided center and bounding box.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="elevation"></param>
        /// <param name="userDefinedCenter"></param>
        /// <param name="userDefinedBoundingBox"></param>
        public virtual GeographicPositionDto GetGeographicPositionDto(IMustHaveGeographicPosition location)
        {
            Guard.Against<ArgumentException>(location == null, "location cannot be null.");
            GeographicPositionDto position = new GeographicPositionDto(
                location.GeoJson, 
                location.Elevation
            );
            return position;
        }

        /// <summary>
        /// Gets the geographic position DTO representation of the item, as well as any user provided center and bounding box.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="elevation"></param>
        /// <param name="userDefinedCenter"></param>
        /// <param name="userDefinedBoundingBox"></param>
        public virtual GeographicPositionDto GetGeographicPositionDto(string geoJson)
        {
            Guard.Against<ArgumentException>(string.IsNullOrEmpty(geoJson), "geoJson must not be null or empty.");
            GeographicPositionDto position = GetGeographicPositionDto(geoJson, null, null, null);
            return position;
        }

        /// <summary>
        /// Gets the geographic position DTO representation of the item, as well as any user provided center and bounding box.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="elevation"></param>
        /// <param name="userDefinedCenter"></param>
        /// <param name="userDefinedBoundingBox"></param>
        public virtual GeographicPositionDto GetGeographicPositionDto(string geoJson, double? elevation, Coordinate? userDefinedCenter = null, double[]? userDefinedBoundingBox = null)
        {
            Guard.Against<ArgumentException>(string.IsNullOrEmpty(geoJson), "geoJson must not be null or empty.");
            Guard.Against<ArgumentException>(userDefinedBoundingBox != null && userDefinedBoundingBox.Length != 4, "If provided, bounding box must be [west, south, east, north].");
            Guard.Against<ArgumentException>(double.IsNaN(elevation.Value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);

            Geometry geom = ConvertGeoJsonToGeometry(geoJson);
            Guard.Against<ArgumentException>(geom == null || geom.IsEmpty || !geom.IsValid, "Geometry conversion was null, empty, or invalid.");
            GeographicPositionDto position = GetGeographicPositionDto(geom, elevation, userDefinedCenter, userDefinedBoundingBox);
            return position;
        }

        /// <summary>
        /// Gets the geographic position DTO representation of the item, as well as any user provided center and bounding box.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="elevation"></param>
        /// <param name="userDefinedCenter"></param>
        /// <param name="userDefinedBoundingBox"></param>
        public virtual GeographicPositionDto GetGeographicPositionDto(Geometry geometry, double? elevation, Coordinate? userDefinedCenter = null, double[]? userDefinedBoundingBox = null)
        {
            Guard.Against<ArgumentNullException>(geometry == null || geometry.IsEmpty, "Geometry must be specified.");
            Guard.Against<ArgumentException>(userDefinedBoundingBox != null && userDefinedBoundingBox.Length != 4, "If provided, bounding box must be [west, south, east, north].");
            Guard.Against<ArgumentException>(double.IsNaN(elevation.Value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);

            // If the geometry is a Point, validate its coordinates
            if (geometry is Point point)
            {
                ValidateWgs84Point(point, true); // Throws an exception if invalid
            }
            GeographicPositionDto position = new GeographicPositionDto(geometry, elevation, userDefinedBoundingBox, userDefinedCenter);
            return position;
        }


        public virtual H3Index? CalculateH3CellFromCoordinate(Geometry geometry, Coordinate userProvidedCenter = null, int resolution = 9)
        {
            H3Index h3Index = null;
            // calculate the h3 cell
            Coordinate center = userProvidedCenter;

            // Fallback to _geometry if _userDefinedCenter is null
            if (center == null)
            {
                if (geometry != null)
                {
                    // Use PointOnSurface as a fallback
                    center = geometry.PointOnSurface?.Coordinate;

                    // If PointOnSurface is null, fallback to Centroid
                    if (center == null)
                    {
                        center = geometry.Centroid?.Coordinate;
                    }
                }
            }
            if (center != null)
            {
                h3Index = center.ToH3Index(resolution);
            }
            return h3Index;
        }
    }
}

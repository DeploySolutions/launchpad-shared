﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="GeographicPosition.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion



namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    using Deploy.LaunchPad.Core;
    using Deploy.LaunchPad.Util;
    using H3;
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;
    using NetTopologySuite.Geometries;
    using Deploy.LaunchPad.Core.Geospatial;

    /// <summary>
    /// This class defines the physical position of something, in terms of its latitude, longitude, and elevation.
    /// </summary>
    [Serializable()]
    public partial class GeographicPosition : IHaveGeographicPosition, IEquatable<GeographicPosition>
    {
        protected readonly GeospatialHelper _helper = new GeospatialHelper();

        /// <summary>
        /// The geometry
        /// </summary>        
        protected Geometry _geometry;

        /// <summary>
        /// The user defined center coordinate. If it's null, will be determined from the geometry.
        /// </summary>
        protected Coordinate _userDefinedCenter;

        /// <summary>
        /// The user defined bounding box. If it's null, will be determined from the geometry.
        /// </summary>
        protected double[] _userDefinedBoundingBox;


        public virtual bool IsPoint => _geometry is Point;

        public virtual bool IsArea => _geometry is Polygon or MultiPolygon;



        /// <summary>
        /// Gets or sets the geo json.
        /// </summary>
        /// <value>The geo json.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string GeoJson
        {
            get; protected set;
        }


        /// <summary>
        /// The elevation
        /// </summary>
        protected double? _elevation;

        /// <summary>
        /// Gets or sets the elevation.
        /// </summary>
        /// <value>The elevation.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double? Elevation
        {
            get { return _elevation; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value.Value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);
                _elevation = value;
            }
        }

        ///<summary>
        /// Describes central latitude (Y) based on the representative coordinate of the item
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Latitude => _helper.GetRepresentativeCoordinate(_geometry, _userDefinedCenter).Y;

        ///<summary>
        /// Describes central longitude (X) based on the representative coordinate of the item
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Longitude => _helper.GetRepresentativeCoordinate(_geometry, _userDefinedCenter).X;

        public virtual double CenterLatitude => _helper.GetCentroid(_geometry).Y;
        public virtual double CenterLongitude => _helper.GetCentroid(_geometry).X;


        /// <summary>
        /// Gets the representative point of the geographic position as a tuple of latitude and longitude.
        /// In NetTopologySuite, geometry.PointOnSurface returns a point guaranteed to lie within the geometry (unlike Centroid, which may fall outside a polygon).
        /// Useful when: You want a "safe for labeling" or "safe for hit-testing" point. You need a representative location inside the area(e.g., for maps, UI, or region tagging).        
        /// Caller's choice: representative vs centroid
        /// </summary>
        public (double Latitude, double Longitude) RepresentativePoint => (Latitude, Longitude);

        /// <summary>
        /// Gets the centroid of the geographic position as a tuple of latitude and longitude.
        /// For a Point, .Centroid just returns the same point.
        /// For a Polygon, .Centroid returns the geometric center (center of mass).
        /// It may lie outside the polygon for non-convex shapes.
        /// Caller's choice: representative vs centroid
        /// </summary>
        public (double Latitude, double Longitude) CentroidPoint => (CenterLatitude, CenterLongitude);

        /// <summary>
        /// Return the user defined bounding box, if any. Otherwise, return the bounding box of the geometry
        /// </summary>
        public virtual double[] BoundingBox
        {
            get
            {
                if (_userDefinedBoundingBox != null)
                    return _userDefinedBoundingBox;

                if (_geometry == null) return null;

                var envelope = _geometry.EnvelopeInternal;
                return
                [
                    envelope.MinX, envelope.MinY, envelope.MaxX, envelope.MaxY
                ];
            }
        }
        
        /// <summary>
        /// The default location is always Greenwich.
        /// </summary>
        public GeographicPosition()
        {
            // We will default to the elevation, longitude and latitude of Greenwich
            GeographicPositionDto position = _helper.GetGeographicPositionDto(new Point(new Coordinate(51.476852, -0.000500)), 46);
            _geometry = position.Geometry;
            _elevation = position.Elevation;
            GeoJson = position.GeoJson;
            _userDefinedBoundingBox = position.UserDefinedBoundingBox;
            _userDefinedCenter = position.UserDefinedCenter;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public GeographicPosition(Geometry geometry, double? elevation, Coordinate userDefinedCenter = null, double[] userDefinedBoundingBox = null)
        {
            GeographicPositionDto position = _helper.GetGeographicPositionDto(geometry, elevation, userDefinedCenter, userDefinedBoundingBox);
            _geometry = position.Geometry;
            _elevation = position.Elevation;
            GeoJson = position.GeoJson;
            _userDefinedBoundingBox = position.UserDefinedBoundingBox;
            _userDefinedCenter = position.UserDefinedCenter;
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public GeographicPosition(SerializationInfo info, StreamingContext context)
        {
            // Deserialize GeoJson and reconstruct _geometry
            GeoJson = info.GetString("GeoJson");
            Geometry geom = null;
            if (!string.IsNullOrEmpty(GeoJson))
            {
                geom = _helper.ConvertGeoJsonToGeometry(GeoJson); // Reconstruct _geometry from GeoJson
            }
            double? elevation = (double?)info.GetValue("Elevation", typeof(double?));
            Coordinate userDefinedCenter = (Coordinate)info.GetValue("Coordinate", typeof(Coordinate));
            double[] userDefinedBoundingBox = (double[])info.GetValue("UserBoundingBox", typeof(double[]));

            GeographicPositionDto position = _helper.GetGeographicPositionDto(geom, elevation, userDefinedCenter, userDefinedBoundingBox);
            _geometry = position.Geometry;
            _elevation = position.Elevation;
            GeoJson = position.GeoJson;
            _userDefinedBoundingBox = position.UserDefinedBoundingBox;
            _userDefinedCenter = position.UserDefinedCenter;
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Elevation", Elevation);
            info.AddValue("GeoJson", GeoJson);
            info.AddValue("Coordinate", _userDefinedCenter); // Serialize backing field for Latitude/Longitude
            info.AddValue("UserBoundingBox", _userDefinedBoundingBox);
        }

        /// <summary>
        /// finite resources that
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other
        public virtual void OnDeserialization(object sender)
        { 
            // Reconstruct _geometry from GeoJson
            if (!string.IsNullOrEmpty(GeoJson))
            {
                GeospatialHelper helper = new GeospatialHelper();
                _geometry = helper.ConvertGeoJsonToGeometry(GeoJson);
            }
            // Recalculate derived properties if needed
           
            // reconnect connection strings and other resources that won't be serialized
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[GeographicLocation : ");
            sb.Append(string.Format("Longitude: {0}", Longitude));
            sb.Append(string.Format("Latitude: {0}", Latitude));
            sb.Append(string.Format("Elevation: {0}", Elevation));
            sb.Append(string.Format("BoundingBox: {0}", BoundingBox));
            sb.Append(string.Format("GeoJson: {0}", GeoJson));
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast objectToSerialize in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) the right type</param>
        /// <returns>True if the objects are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is GeographicPosition)
            {
                return Equals(obj as GeographicPosition);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type. We need to test for
        /// property equality - which in this case means the comparing double vlaues to each other, which is imprecise. So we need to accept some small level of imprecision
        /// </summary>
        /// <param name="obj">The other object of this type we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(GeographicPosition obj)
        {
            if (obj == null)
            {
                return false;
            }

            // Compare Elevation with a small tolerance for imprecision
            bool elevationEqual = Elevation.HasValue && obj.Elevation.HasValue
                ? Math.Abs(Elevation.Value - obj.Elevation.Value) < 0.0001
                : Elevation == obj.Elevation; // Handles cases where one or both are null

            // Compare _geometry using EqualsExact (or EqualsTopologically if needed)
            bool geometryEqual = _geometry != null && obj._geometry != null
                ? _geometry.EqualsExact(obj._geometry)
                : _geometry == obj._geometry; // Handles cases where one or both are null

            // Return true only if both Elevation and Geometry are equal
            return elevationEqual && geometryEqual;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(GeographicPosition x, GeographicPosition y)
        {
            if (ReferenceEquals(x, null))
            {
                if (ReferenceEquals(y, null))
                {
                    return true;
                }
                return false;
            }
            return x.Equals(y);
        }

        /// <summary>
        /// Override the != operator to test for inequality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are not equal based on the Equals logic</returns>
        public static bool operator !=(GeographicPosition x, GeographicPosition y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Elevation.GetHashCode() + _userDefinedCenter.GetHashCode() + _geometry.GetHashCode();
        }
    }


}

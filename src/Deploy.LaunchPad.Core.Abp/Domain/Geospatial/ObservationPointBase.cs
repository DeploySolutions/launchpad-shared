// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ObservationPointBase.cs" company="Deploy Software Solutions, inc.">
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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Geospatial;
using Deploy.LaunchPad.Util;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// This class defines the geographical boundaries of an Area of Interest being observed.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TParentAreaOfInterest">The type of the t parent area of interest.</typeparam>
    [Serializable()]
    public abstract partial class ObservationPointBase<TIdType, TParentAreaOfInterest> :
        LaunchPadDomainEntityBase<TIdType>, IObservationPoint<TParentAreaOfInterest>, IMayHaveTenant
        where TParentAreaOfInterest : IAreaOfInterest
    {
        #region "Geographic Properties"

        protected readonly GeospatialHelper _helper = new GeospatialHelper();

        /// <summary>
        /// The geometry
        /// </summary>        
        protected Geometry _geometry;

        /// <summary>
        /// The user defined center coordinate. If it's null, will be determined from the geometry.
        /// </summary>
        protected Coordinate? _userDefinedCenter;

        /// <summary>
        /// The user defined bounding box. If it's null, will be determined from the geometry.
        /// </summary>
        protected double[]? _userDefinedBoundingBox;


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
        public virtual System.Double Latitude => _helper.GetRepresentativeCoordinate(_geometry, _userDefinedCenter).Y;

        ///<summary>
        /// Describes central longitude (X) based on the representative coordinate of the item
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual System.Double Longitude => _helper.GetRepresentativeCoordinate(_geometry, _userDefinedCenter).X;

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
        public virtual double[]? BoundingBox
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
        /// Sets the geographic position of the item, as well as any user provided center and bounding box.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="elevation"></param>
        /// <param name="userDefinedCenter"></param>
        /// <param name="userDefinedBoundingBox"></param>
        public virtual void SetGeographicPosition(string geoJson, double? elevation, Coordinate? userDefinedCenter = null, double[]? userDefinedBoundingBox = null)
        {
            Guard.Against<ArgumentException>(string.IsNullOrEmpty(geoJson), "geoJson must not be null or empty.");
            Guard.Against<ArgumentException>(userDefinedBoundingBox != null && userDefinedBoundingBox.Length != 4, "If provided, bounding box must be [west, south, east, north].");
            Guard.Against<ArgumentException>(double.IsNaN(elevation.Value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);

            Geometry geom = _helper.SetGeometryFromGeoJson(geoJson);
            Guard.Against<ArgumentException>(geom == null || geom.IsEmpty || !geom.IsValid, "Geometry conversion was null, empty, or invalid.");

            _geometry = geom;
            _elevation = elevation;
            _userDefinedBoundingBox = userDefinedBoundingBox;
            _userDefinedCenter = userDefinedCenter;
        }

        /// <summary>
        /// Sets the geographic position of the item, as well as any user provided center and bounding box.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="elevation"></param>
        /// <param name="userDefinedCenter"></param>
        /// <param name="userDefinedBoundingBox"></param>
        public virtual void SetGeographicPosition(Geometry geometry, double? elevation, Coordinate? userDefinedCenter = null, double[]? userDefinedBoundingBox = null)
        {
            Guard.Against<ArgumentNullException>(geometry == null || geometry.IsEmpty || !geometry.IsValid, "Geometry must be specified and valid.");
            Guard.Against<ArgumentException>(userDefinedBoundingBox != null && userDefinedBoundingBox.Length != 4, "If provided, bounding box must be [west, south, east, north].");
            Guard.Against<ArgumentException>(double.IsNaN(elevation.Value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);
            _geometry = geometry;
            _elevation = elevation;
            _userDefinedBoundingBox = userDefinedBoundingBox;
            _userDefinedCenter = userDefinedCenter;
        }

        #endregion

        /// <summary>
        /// Gets or sets the parent aoi.
        /// </summary>
        /// <value>The parent aoi.</value>
        public virtual TParentAreaOfInterest? ParentAoi { get; set; }

        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationPointBase{TIdType, TParentAreaOfInterest}"/> class.
        /// </summary>
        protected ObservationPointBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationPointBase{TIdType, TParentAreaOfInterest}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected ObservationPointBase(int? tenantId) : base()
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationPointBase{TIdType, TParentAreaOfInterest}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="parentAoi">The parent aoi.</param>
        protected ObservationPointBase(int? tenantId, TParentAreaOfInterest parentAoi) : base()
        {
            TenantId = tenantId;
            ParentAoi = parentAoi;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationPointBase{TIdType, TParentAreaOfInterest}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="location">The location.</param>
        protected ObservationPointBase(int? tenantId, IHaveGeographicPosition location) : base()
        {
            TenantId = tenantId;
            GeoJson = location.GeoJson;

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public ObservationPointBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            GeoJson = info.GetString("GeoJson");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("GeoJson", GeoJson);
        }

        /// <summary>
        /// finite resources that
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other
        public override void OnDeserialization(object sender)
        {
            // reconnect connection strings and other resources that won't be serialized
        }


        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[AreaOfInterest : ");
            // sb.AppendFormat(base.ToStringBaseProperties());
            sb.AppendFormat("GeoJson={0};", GeoJson);
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
            if (obj != null && obj is ObservationPointBase<TIdType, TParentAreaOfInterest>)
            {
                return Equals(obj as ObservationPointBase<TIdType, TParentAreaOfInterest>);
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
        public bool Equals(ObservationPointBase<TIdType, TParentAreaOfInterest> obj)
        {
            if (obj != null)
            {
                if (
                    
                    GeoJson.Equals(obj.GeoJson)
                )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(ObservationPointBase<TIdType, TParentAreaOfInterest> x, ObservationPointBase<TIdType, TParentAreaOfInterest> y)
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
        public static bool operator !=(ObservationPointBase<TIdType, TParentAreaOfInterest> x, ObservationPointBase<TIdType, TParentAreaOfInterest> y)
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
            return Id.GetHashCode() + Culture.GetHashCode() + GeoJson.GetHashCode();
        }

    }
}

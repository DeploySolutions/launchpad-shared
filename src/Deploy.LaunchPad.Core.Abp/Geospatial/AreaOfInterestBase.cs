// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="AreaOfInterestBase.cs" company="Deploy Software Solutions, inc.">
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
using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.Geospatial;
using Deploy.LaunchPad.Geospatial.Position;
using Deploy.LaunchPad.Geospatial.ReferencePoint;
using Deploy.LaunchPad.Core.Guids;
using NetTopologySuite.Geometries;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using IMayHaveTenant = Deploy.LaunchPad.Core.Metadata.IMayHaveTenant;

namespace Deploy.LaunchPad.Core.Abp.Geospatial
{

    /// <summary>
    /// This class defines the geographical boundaries of an Area of Interest being observed.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    [Serializable()]
    public abstract partial class AreaOfInterestBase<TIdType> :
        LaunchPadDomainEntityBase<TIdType>, IAreaOfInterest, IMayHaveTenant
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


        // IMayHaveElevation

        /// <summary>
        /// Gets or sets the elevation.
        /// </summary>
        /// <value>The elevation.</value>
        public Elevation? Elevation { get; set; }


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

        private double? _representativeLatitude;
        private double? _representativeLongitude;
        private double? _centroidLatitude;
        private double? _centroidLongitude;

        public virtual double RepresentativeLatitude
        {
            get => _representativeLatitude ?? _helper.GetRepresentativeCoordinate(_geometry, _userDefinedCenter).Y;
            set => _representativeLatitude = value;
        }

        public virtual double RepresentativeLongitude
        {
            get => _representativeLongitude ?? _helper.GetRepresentativeCoordinate(_geometry, _userDefinedCenter).X;
            set => _representativeLongitude = value;
        }

        public virtual double CentroidLatitude
        {
            get => _centroidLatitude ?? _helper.GetCentroid(_geometry).Y;
            set => _centroidLatitude = value;
        }

        public virtual double CentroidLongitude
        {
            get => _centroidLongitude ?? _helper.GetCentroid(_geometry).X;
            set => _centroidLongitude = value;
        }

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

        ///<summary>
        /// Country Id GUID
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual System.Guid CountryId { get; set; } = GuidConstants.Unknown;


        #endregion

        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaOfInterestBase{TIdType}"/> class.
        /// </summary>
        protected AreaOfInterestBase() : base()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AreaOfInterestBase{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected AreaOfInterestBase(int? tenantId) : base()
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaOfInterestBase{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="geoJson">The geo json.</param>
        protected AreaOfInterestBase(int? tenantId, string geoJson) : base()
        {
            TenantId = tenantId;
            GeospatialHelper helper = new GeospatialHelper();
            var position = helper.GetGeographicPositionDto(geoJson);
            GeoJson = position.GeoJson;
            _geometry = position.Geometry;
            Elevation = position.Elevation;
            _userDefinedBoundingBox = position.BoundingBox;
            _userDefinedCenter = position.UserDefinedCenter;

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public AreaOfInterestBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            GeoJson = info.GetString("GeoJson");
            Elevation = (Elevation)info.GetValue("Elevation", typeof(Elevation));
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
            info.AddValue("Elevation", Elevation);
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
            sb.AppendFormat("Elevation={0};", Elevation);
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
            if (obj != null && obj is AreaOfInterestBase<TIdType>)
            {
                return Equals(obj as AreaOfInterestBase<TIdType>);
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
        public bool Equals(AreaOfInterestBase<TIdType> obj)
        {
            if (obj != null)
            {
                if (
                    GeoJson.Equals(obj.GeoJson) && Elevation.Equals(obj.Elevation)
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
        public static bool operator ==(AreaOfInterestBase<TIdType> x, AreaOfInterestBase<TIdType> y)
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
        public static bool operator !=(AreaOfInterestBase<TIdType> x, AreaOfInterestBase<TIdType> y)
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

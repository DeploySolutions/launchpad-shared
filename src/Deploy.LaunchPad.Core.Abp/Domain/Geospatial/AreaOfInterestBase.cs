// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="AreaOfInterestBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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
using Deploy.LaunchPad.Core.Util;
using H3;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// This class defines the geographical boundaries of an Area of Interest being observed.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    [Serializable()]
    public abstract partial class AreaOfInterestBase<TIdType> :
        LaunchPadDomainEntityBase<TIdType>, IAreaOfInterest, IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// The geometry
        /// </summary>
        [NotMapped]
        protected Polygon _geometry;

        /// <summary>
        /// Sets the geometry.
        /// </summary>
        /// <returns>Polygon.</returns>
        public virtual Polygon SetGeometry()
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringReader = new StringReader(GeoJson))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                _geometry = serializer.Deserialize<Polygon>(jsonReader);
            }
            return _geometry;
        }


        /// <summary>
        /// Gets or sets the geo json.
        /// </summary>
        /// <value>The geo json.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength]
        public virtual string? GeoJson { get; set; }

        /// <summary>
        /// The earth coordinate
        /// </summary>
        protected Coordinate _earthCoordinate;
        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Coordinate Coordinate
        {
            get
            {
                return _earthCoordinate;
            }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value.X), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_NaN);
                Guard.Against<ArgumentOutOfRangeException>(value.X > 180, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_GreaterThan_180);
                Guard.Against<ArgumentOutOfRangeException>(value.X < -180, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_LessThan_Minus180);
                Guard.Against<ArgumentException>(double.IsNaN(value.Y), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_NaN);
                Guard.Against<ArgumentOutOfRangeException>(value.Y > 90, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_GreaterThan_90);
                Guard.Against<ArgumentOutOfRangeException>(value.Y < -90, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90);
                Guard.Against<ArgumentOutOfRangeException>(value.Y < -90, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90);
                Guard.Against<ArgumentOutOfRangeException>(value.Y < -90, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90);
                _earthCoordinate = value;
            }
        }

        /// <summary>
        /// The h3 index
        /// </summary>
        protected H3Index? _h3Index;
        /// <summary>
        /// Gets or sets the index of the h3.
        /// </summary>
        /// <value>The index of the h3.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual H3Index? H3Index
        {
            get
            {
                return _h3Index;
            }
            set
            {
                _h3Index = value;
            }
        }

        /// <summary>
        /// The altitude
        /// </summary>
        protected double? _altitude;
        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double? Altitude
        {
            get { return _altitude; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value.Value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Altitude);
                _altitude = value;
            }
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
            GeoJson = geoJson;

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public AreaOfInterestBase(SerializationInfo info, StreamingContext context) : base(info, context)
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

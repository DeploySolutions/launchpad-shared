//LaunchPad Shared
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Types;
using Deploy.LaunchPad.Core.GeoJson;
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
    [Serializable()]
    public abstract partial class AreaOfInterestBase<TIdType, TGeoJsonType> :
        LaunchPadDomainEntityBase<TIdType>, IAreaOfInterest<TIdType, TGeoJsonType>, IMayHaveTenant
        where TGeoJsonType : IAmAGeometryType, new()
    {

        public virtual int? TenantId { get; set; }
        public TGeoJsonType Definition { get; set; }
        GeoJsonId? ICanBeDescribedInGeoJson<TGeoJsonType>.Id { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public AreaOfInterestBase(int? tenantId) : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            TenantId = tenantId;
        }

        public AreaOfInterestBase(int? tenantId, IGeographicPosition location) : base()
        {
            
        }
        public AreaOfInterestBase(int? tenantId, TIdType id, String culture, IGeographicPosition location) : base(id, culture)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public AreaOfInterestBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Definition = (TGeoJsonType)info.GetValue("Definition", typeof(TGeoJsonType));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Definition", Definition);
        }

        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other 
        /// <summary>finite resources that 
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
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
            sb.AppendFormat("Definition={0};", Definition);
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
            if (obj != null && obj is AreaOfInterestBase<TIdType, TGeoJsonType>)
            {
                return Equals(obj as AreaOfInterestBase<TIdType, TGeoJsonType>);
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
        /// <returns></returns>
        public bool Equals(AreaOfInterestBase<TIdType, TGeoJsonType> obj)
        {
            if (obj != null)
            {
                if (
                    Definition.Equals(obj.Definition)
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
        public static bool operator ==(AreaOfInterestBase<TIdType, TGeoJsonType> x, AreaOfInterestBase<TIdType, TGeoJsonType> y)
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
        public static bool operator !=(AreaOfInterestBase<TIdType, TGeoJsonType> x, AreaOfInterestBase<TIdType, TGeoJsonType> y)
        {
            return !(x == y);
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + Culture.GetHashCode() + Definition.GetHashCode();
        }
    }
}

//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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



namespace DeploySoftware.LaunchPad.Core.Domain
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    /// <summary>
    /// This class defines the geographical boundaries of an Area of Interest being observed.
    /// </summary>
    [Serializable()]
    public class AreaOfInterest<TIdType> : 
        DomainEntityBase<TIdType>, IAreaOfInterest<TIdType>
    {
        
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IEnumerable<IGeographicLocation> BoundingBoxCoordinates
        {
            get;set;
        }


        /// <summary>
        /// 
        /// </summary>
        public AreaOfInterest(int? tenantId) :base (tenantId)
        {
            
        }

        public AreaOfInterest(int? tenantId, IEnumerable<IGeographicLocation> boundingBox) : base(tenantId)
        {
            BoundingBoxCoordinates = boundingBox;
        }
        public AreaOfInterest(int? tenantId, TIdType id, String culture, IEnumerable<IGeographicLocation> boundingBox) : base(tenantId, id, culture)
        {
            BoundingBoxCoordinates = boundingBox;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public AreaOfInterest(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            BoundingBoxCoordinates = (IEnumerable<IGeographicLocation>)info.GetValue("BoundingBoxCoordinates", typeof(IEnumerable<IGeographicLocation>));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("BoundingBoxCoordinates", BoundingBoxCoordinates);
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
            sb.AppendFormat(base.ToStringBaseProperties());
            sb.AppendFormat("BoundingBoxCoordinates={0};", BoundingBoxCoordinates);
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast objectToSerialize in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) the right type</param>
        /// <returns>True if the objects are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is AreaOfInterest<TIdType>)
            {
                return Equals(obj as AreaOfInterest<TIdType>);
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
        public bool Equals(AreaOfInterest<TIdType> obj)
        {
            if (obj != null)
            {
                if (
                    BoundingBoxCoordinates.Equals(obj.BoundingBoxCoordinates)
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
        public static bool operator ==(AreaOfInterest<TIdType> x, AreaOfInterest<TIdType> y)
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
        public static bool operator !=(AreaOfInterest<TIdType> x, AreaOfInterest<TIdType> y)
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
            return Id.GetHashCode() + Key.Culture.GetHashCode() + BoundingBoxCoordinates.GetHashCode();
        }
    }
}

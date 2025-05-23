﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-12-2023
// ***********************************************************************
// <copyright file="SpaceTimeInformation.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Geospatial
{
    using Deploy.LaunchPad.Core.Geospatial.Position;
    using Deploy.LaunchPad.Core.Temporal;
    using Deploy.LaunchPad.Util;
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// This class allows us to track the location of an entity in terms of its location in space and time.
    /// </summary>
    [Serializable()]
    public partial class SpaceTimeInformation : IEquatable<SpaceTimeInformation>, IMayHaveTemporalInformation
    {

        /// <summary>
        /// The date and time extents that this object was present in this physical location
        /// </summary>
        /// <value>The point in time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual TemporalInformation TemporalInformation { get; set; }

        /// <summary>
        /// The geographic location of this item at a point in time
        /// </summary>
        /// <value>The physical location.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IMustHaveGeographicPosition PhysicalLocation { get; set; }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceTimeInformation">SpaceTimeInformation</see> class.
        /// </summary>
        public SpaceTimeInformation()
        {
            PhysicalLocation = new GeographicPosition();
            TemporalInformation = new TemporalInformation();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceTimeInformation">SpaceTimeInformation</see> class.
        /// <param name="location">The specific location that is occupied at this moment</param>
        /// </summary>
        /// <param name="location">The location.</param>
        public SpaceTimeInformation(GeographicPosition location)
        {
            PhysicalLocation = location;
            TemporalInformation = new TemporalInformation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceTimeInformation">SpaceTimeInformation</see> class.
        /// <param name="pointInTime">A specific time that this location refers to</param>
        /// </summary>
        /// <param name="pointInTime">The point in time.</param>
        public SpaceTimeInformation(DateTime pointInTime)
        {
            PhysicalLocation = new GeographicPosition();
            TemporalInformation = new TemporalInformation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceTimeInformation">SpaceTimeInformation</see> class.
        /// <param name="location">The specific location that is occupied at this moment</param><param name="pointInTime">A specific time that this location refers to</param>
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="pointInTime">The point in time.</param>
        public SpaceTimeInformation(GeographicPosition location, TemporalInformation pointInTime)
        {
            PhysicalLocation = location;
            TemporalInformation = pointInTime;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public SpaceTimeInformation(SerializationInfo info, StreamingContext context)
        {
            PhysicalLocation = (IMustHaveGeographicPosition)info.GetValue("PhysicalLocation", typeof(IMustHaveGeographicPosition));
            TemporalInformation = (TemporalInformation)info.GetValue("TemporalInformation", typeof(TemporalInformation));
        }

        #endregion
        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PhysicalLocation", PhysicalLocation);
            info.AddValue("TemporalInformation", TemporalInformation);
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
            // reconnect connection strings and other resources that won't be serialized
        }


        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[SpaceTimeInformation : ");
            sb.AppendFormat("PhysicalLocation={0};", PhysicalLocation);
            sb.AppendFormat("TemporalInformation={0};", TemporalInformation);
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
            if (obj != null && obj is SpaceTimeInformation)
            {
                return Equals(obj as SpaceTimeInformation);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type. We need to test for
        /// business key equality - which in this case means the Location field.
        /// </summary>
        /// <param name="obj">The other object of this type we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(SpaceTimeInformation obj)
        {
            if (obj != null)
            {
                if (PhysicalLocation.Equals(obj.PhysicalLocation) &&
                    TemporalInformation.Equals(obj.TemporalInformation)
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
        public static bool operator ==(SpaceTimeInformation x, SpaceTimeInformation y)
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
        /// Implements the != operator.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SpaceTimeInformation x, SpaceTimeInformation y)
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
            return PhysicalLocation.GetHashCode() + TemporalInformation.GetHashCode();
        }

    }
}
//LaunchPad Shared
// Copyright (c) 2018-2022 Deploy Software Solutions, inc. 

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
    using DeploySoftware.LaunchPad.Core.Util;
    using CoordinateSharp;
    using DeploySoftware.LaunchPad.Core;

    /// <summary>
    /// This class defines the physical position of something, in terms of its latitude, longitude, and elevation.
    /// </summary>
    [Serializable()]
    public class GeographicLocation : IGeographicLocation, IEquatable<GeographicLocation>
    {
        private double _elevation;
        private Coordinate _earthCoordinate;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Coordinate EarthCoordinate
        {
            get => _earthCoordinate;
            set => _earthCoordinate = value;
        }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Elevation
        {
            get { return _elevation; }
            set
            {
                Guard.Against< ArgumentException>(double.IsNaN(value), DeploySoftware_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);
                _elevation = value;
            }
        }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Latitude => _earthCoordinate.Latitude.ToDouble();

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Longitude => _earthCoordinate.Longitude.ToDouble();

        /// <summary>
        /// The default location is always Greenwich.
        /// </summary>
        public GeographicLocation()
        {
            // We will set the elevation, latitude, and longitude of Greenwich
            _elevation = 46;
            EagerLoad load = new EagerLoad
            {
                Cartesian = false,
                Celestial = false,
                UTM_MGRS = false
            };
            EarthCoordinate = new Coordinate(51.476852, -0.000500, new DateTime(2000, 1, 1).ToUniversalTime(), load);
        }

        public GeographicLocation(double latitude, double longitude)
        {
            Elevation = 0;
            EagerLoad load = new EagerLoad
            {
                Cartesian = false,
                Celestial = false,
                UTM_MGRS = false
            };
            EarthCoordinate = new Coordinate(latitude, longitude, load);
        }

        public GeographicLocation(double latitude, double longitude, EagerLoad load)
        {
            Elevation = 0;
            EarthCoordinate = new Coordinate(latitude, longitude, load);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public GeographicLocation(SerializationInfo info, StreamingContext context)
        {
            Elevation = (double)info.GetDouble("Elevation");
            EarthCoordinate = (Coordinate)info.GetValue("EarthCoordinate",typeof(Coordinate));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Elevation", Elevation);
            info.AddValue("Latitude", Latitude);
            info.AddValue("Longitude", Longitude);
            info.AddValue("EarthCoordinate", EarthCoordinate);
        }

        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other 
        /// <summary>finite resources that 
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
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
            sb.Append("[GeographicLocation : ");
            sb.AppendFormat("Elevation={0};", Elevation);
            sb.AppendFormat("Latitude={0};", Latitude);
            sb.AppendFormat(" Longitude={0};", Longitude);
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
            if (obj != null && obj is GeographicLocation)
            {
                return Equals(obj as GeographicLocation);
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
        public bool Equals(GeographicLocation obj)
        {
            if (obj != null)
            {
                if (
                    Math.Abs(Elevation - obj.Elevation) < 0.0001
                    && Math.Abs(Latitude - obj.Latitude) < 0.0001
                    && Math.Abs(Longitude - obj.Longitude) < 0.0001
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
        public static bool operator ==(GeographicLocation x, GeographicLocation y)
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
        public static bool operator !=(GeographicLocation x, GeographicLocation y)
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
            return Elevation.GetHashCode() + Latitude.GetHashCode() + Longitude.GetHashCode();
        }
    }
}

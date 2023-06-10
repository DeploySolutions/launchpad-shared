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



namespace Deploy.LaunchPad.Core.Domain
{
    using Deploy.LaunchPad.Core;
    using Deploy.LaunchPad.Core.GeoJson;
    using Deploy.LaunchPad.Core.Util;
    using H3;
    using System;
    using System.ComponentModel;
    using System.Numerics;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using H3.Extensions;
    using NetTopologySuite.Geometries;

    /// <summary>
    /// This class defines the physical position of something, in terms of its latitude, longitude, and elevation.
    /// </summary>
    [Serializable()]
    public partial class GeographicPosition :  IGeographicPosition, IEquatable<GeographicPosition>
    {
        private double _elevation;
        private Coordinate _earthCoordinate;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Elevation
        {
            get { return _elevation; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);
                _elevation = value;
            }
        }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Latitude
        {
            get
            {
                return _earthCoordinate.Y;
            }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_NaN);
                Guard.Against<ArgumentOutOfRangeException>(value > 90, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_GreaterThan_90);
                Guard.Against<ArgumentOutOfRangeException>(value < -90, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90);
                _earthCoordinate.Y = value;
            }
        }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Longitude
        {
            get
            {
                return _earthCoordinate.X;
            }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_NaN);
                Guard.Against<ArgumentOutOfRangeException>(value > 180, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_GreaterThan_180);
                Guard.Against<ArgumentOutOfRangeException>(value < -180, Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_LessThan_Minus180);
                _earthCoordinate.X = value;
            }
        }

        protected H3Index _h3Index;
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual H3Index H3Index
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
        /// The default location is always Greenwich.
        /// </summary>
        public GeographicPosition()
        {
            // We will set the elevation, longitude and latitude of Greenwich
            _elevation = 46;
            _earthCoordinate = new Coordinate(51.476852, -0.000500);
            // calculate the h3 cell
            _h3Index = _earthCoordinate.ToH3Index(9);
        }

        public GeographicPosition(double longitude, double latitude)
        {
            Elevation = 0;
            Latitude = latitude; 
            Longitude = longitude;
            // calcualte the h3 cell
            var coordinate = new NetTopologySuite.Geometries.Coordinate(longitude, latitude);
            _h3Index = coordinate.ToH3Index(9);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public GeographicPosition(SerializationInfo info, StreamingContext context)
        {
            Elevation = (double)info.GetDouble("Elevation");
            Longitude = (double)info.GetDouble("Longitude");
            Latitude = (double)info.GetDouble("Latitude");
            H3Index = (H3Index)info.GetDouble("H3Index");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Elevation", Elevation);
            info.AddValue("Longitude", Longitude);
            info.AddValue("Latitude", Latitude);
            info.AddValue("H3Index", H3Index);
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
            sb.Append(string.Format("Elevation: {0}", Elevation));
            sb.Append(string.Format("Longitude: {0}", Longitude));
            sb.Append(string.Format("Latitude: {0}", Latitude));
            sb.Append(string.Format("H3Index: {0}", H3Index));
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
        /// <returns></returns>
        public bool Equals(GeographicPosition obj)
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
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Elevation.GetHashCode() + Longitude.GetHashCode() + Latitude.GetHashCode();
        }
    }


}

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



namespace Deploy.LaunchPad.Core.Geospatial
{
    using Deploy.LaunchPad.Core;
    using Deploy.LaunchPad.Core.Util;
    using H3;
    using System;
    using System.ComponentModel;
    using System.Numerics;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using NetTopologySuite.Geometries;
    using global::H3;
    using global::H3.Extensions;
    using NetTopologySuite.IO;
    using Newtonsoft.Json;
    using System.IO;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// This class defines the physical position of something, in terms of its latitude, longitude, and elevation.
    /// </summary>
    [Serializable()]
    public partial class GeographicPosition : IHaveGeographicPosition, IEquatable<GeographicPosition>
    {
        [NotMapped]
        protected Point _geometry;

        public virtual Point SetGeometry()
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringReader = new StringReader(GeoJson))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                _geometry = serializer.Deserialize<Point> (jsonReader);
            }
            return _geometry;
        }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string GeoJson
        {
            get; set;
        }


        protected double _elevation;
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


        protected Coordinate _earthCoordinate;
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
            Coordinate = new Coordinate(51.476852, -0.000500);
            // calculate the h3 cell
            _h3Index = _earthCoordinate.ToH3Index(9);
        }

        public GeographicPosition(double longitude, double latitude)
        {
            Elevation = 0;
            Coordinate = new Coordinate(longitude, latitude);
            // calcualte the h3 cell
            _h3Index = Coordinate.ToH3Index(9);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public GeographicPosition(SerializationInfo info, StreamingContext context)
        {
            Elevation = (double)info.GetDouble("Elevation");
            Coordinate = (Coordinate)info.GetValue("Coordinate", typeof(Coordinate));
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
            info.AddValue("Coordinate", Coordinate);
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
            sb.Append(string.Format("Coordinate: {0}", Coordinate));
            sb.Append(string.Format("Elevation: {0}", Elevation));
            sb.Append(string.Format("GeoJson: {0}", GeoJson));
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
                    && Math.Abs(Coordinate.X - obj.Coordinate.X) < 0.0001
                    && Math.Abs(Coordinate.Y - obj.Coordinate.Y) < 0.0001
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
        /// This method implements the <see cref="object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Elevation.GetHashCode() + Coordinate.GetHashCode() + H3Index.GetHashCode();
        }
    }


}

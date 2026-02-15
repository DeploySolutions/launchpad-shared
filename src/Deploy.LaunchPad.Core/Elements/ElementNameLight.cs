using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Serialization;
using System;
using Newtonsoft.Json;
using Deploy.LaunchPad.Util.ValueConverters;

namespace Deploy.LaunchPad.Core.Elements
{
    [Serializable]      
    [ComplexType]
    [DebuggerDisplay("{_debugDisplay}")]
    [JsonConverter(typeof(ElementNameLightJsonConverter))]
    public partial class ElementNameLight : IElementNameLight
    {
        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        /// <value>The debug display.</value>
        protected virtual string _debugDisplay => $"{Full}.";


        protected string _full = string.Empty;
        /// <summary>
        /// The full name of this element
        /// </summary>
        /// <value>The full name.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_ElementName_Full_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("full")]
        public virtual string Full
        {
            get
            {
                return _full;
            }
            set
            {
                _full = value;
            }
        }

        public ElementNameLight()
        {
        }

        public ElementNameLight(string fullName)
        {
            Full = fullName;
        }

        public ElementNameLight(string fullName, string shortName)
        {
            Full = fullName;
        }


        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(ElementNameLight other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by DisplayName
            return Full.CompareTo(other.Full)
            ;
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return Full;
        }


        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ElementNameLight)
            {
                return Equals(obj as ElementNameLight);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Equals(ElementNameLight obj)
        {
            if (obj != null)
            {
                return Full.Equals(obj.Full)
                ;
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(ElementNameLight x, ElementNameLight y)
        {
            if (x is null)
            {
                if (y is null)
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
        public static bool operator !=(ElementNameLight x, ElementNameLight y)
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
            return Full.GetHashCode()
            ;
        }


        public ElementNameLight CloneGeneric()
        {
            // Create a new instance and copy all relevant properties
            return new ElementNameLight(
                fullName: this.Full
            );
        }
        object ICloneable.Clone() => CloneGeneric();
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Serialization;
using System;
using Newtonsoft.Json;
using Deploy.LaunchPad.Util.ValueConverters;

namespace Deploy.LaunchPad.Core
{
    [Serializable]      
    [ComplexType]
    [DebuggerDisplay("{_debugDisplay}")]
    public partial class ElementName : ElementNameLight, IElementName
    {
        
        protected string? _prefix;
        /// <summary>
        /// The prefix, if any
        /// </summary>
        /// <value>The prefix of the element.</value>
        [MaxLength(12, ErrorMessageResourceName = "Validation_12CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string? Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                _prefix = value;
            }
        }


        protected string? _suffix;
        /// <summary>
        /// The suffix, if any
        /// </summary>
        /// <value>The suffix of the element.</value>
        [MaxLength(12, ErrorMessageResourceName = "Validation_12CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("suffix", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string? Suffix
        {
            get
            {
                return _suffix;
            }
            set
            {
                _suffix = value;
            }
        }


        protected ElementName() : base()
        {
        }

        public ElementName(string fullName) : base(fullName)
        {
            Full = fullName;
            if (!string.IsNullOrEmpty(fullName))
            {
                Short = fullName.Length > 50 ? fullName.Substring(0, 50) : fullName;
            }
        }

        public ElementName(string fullName, string shortName) : base(fullName, shortName)
        {
            Full = fullName;
            if (!string.IsNullOrEmpty(shortName))
            {
                Short = shortName.Length > 50 ? shortName.Substring(0, 50) : shortName;
            }
        }


        public ElementName(string fullName, string shortName, string prefix, string suffix) : base(fullName, shortName)
        {
            Full = fullName;
            if (!string.IsNullOrEmpty(shortName))
            {
                Short = shortName.Length > 50 ? shortName.Substring(0, 50) : shortName;
            }
            Prefix = prefix;
            Suffix = suffix;
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(ElementName other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by DisplayName
            return Full.CompareTo(other.Full)
                & Short.CompareTo(other.Short)
                & Prefix.CompareTo(other.Prefix)
                & Suffix.CompareTo(other.Suffix)
            ;
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return Short;
        }


        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ElementName)
            {
                return Equals(obj as ElementName);
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
        public virtual bool Equals(ElementName obj)
        {
            if (obj != null)
            {
                return string.Equals(Full, obj.Full, StringComparison.Ordinal) &&
                    string.Equals(Short, obj.Short, StringComparison.Ordinal) &&
                    string.Equals(Prefix, obj.Prefix, StringComparison.Ordinal) &&
                    string.Equals(Suffix, obj.Suffix, StringComparison.Ordinal);
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(ElementName x, ElementName y)
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
        public static bool operator !=(ElementName x, ElementName y)
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
                + Short.GetHashCode()
                + Prefix.GetHashCode()
                + Suffix.GetHashCode()
            ;
        }
    }
}

using Deploy.LaunchPad.Util.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util.Elements
{
    [Serializable]   
    [Owned]
    [DebuggerDisplay("{_debugDisplay}")]
    [JsonConverter(typeof(ElementNameJsonConverter))]
    public partial class ElementName : ElementNameLight, IElementName
    {
        protected string _shortName = string.Empty;
        /// <summary>
        /// The short name of this element (if different from the FullName field). If not set, it will default to the first 50 characters of the full name.
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(50, ErrorMessageResourceName = "Validation_Name_Short_50CharsOrLess", ErrorMessageResourceType = typeof( Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("short", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        [Column("core_name_short")]
        public virtual string ShortName
        {
            get
            {
                if (string.IsNullOrEmpty(_shortName))
                {
                    return Name;
                }
                else
                {
                    return _shortName;
                }
            }
            set
            {
                _shortName = value;
            }
        }

        protected string? _suffix;
        /// <summary>
        /// The suffix of this element, if any (ex. "jr", or "MD")
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(50, ErrorMessageResourceName = "Validation_Name_Short_50CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("suffix", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        [Column("core_name_suffix")]
        public virtual string? Suffix
        {
            get
            {
                if (string.IsNullOrEmpty(_suffix))
                {
                    return Name;
                }
                else
                {
                    return _suffix;
                }
            }
            set
            {
                _suffix = value;
            }
        }

        protected string? _prefix;
        /// <summary>
        /// The prefix of this element, if any (ex. "Dr.")
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(50, ErrorMessageResourceName = "Validation_Name_Short_50CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        [Column("core_name_prefix")]
        public virtual string? Prefix
        {
            get
            {
                if (string.IsNullOrEmpty(_prefix))
                {
                    return Name;
                }
                else
                {
                    return _prefix;
                }
            }
            set
            {
                _prefix = value;
            }
        }

        public IDictionary<string, string> AlternateNames { get; set; }

        [SetsRequiredMembers]
        protected ElementName() : base()
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            AlternateNames = new Dictionary<string, string>(comparer);
            AlternateNames.TryAdd(ShortName, ShortName);
        }

        [SetsRequiredMembers]
        public ElementName(string fullName) : base(fullName)
        {
            Name = fullName;
            if (!string.IsNullOrEmpty(fullName))
            {
                ShortName = fullName.Length > 50 ? fullName.Substring(0, 50) : fullName;
            }
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            AlternateNames = new Dictionary<string, string>(comparer);
            AlternateNames.TryAdd(ShortName, ShortName);
        }

        [SetsRequiredMembers]
        public ElementName(string fullName, string shortName) : base(fullName, shortName)
        {
            Name = fullName;
            if (!string.IsNullOrEmpty(shortName))
            {
                ShortName = shortName.Length > 50 ? shortName.Substring(0, 50) : shortName;
            }
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            AlternateNames = new Dictionary<string, string>(comparer);
            AlternateNames.TryAdd(ShortName, ShortName);
        }


        [SetsRequiredMembers]
        public ElementName(string fullName, string shortName, string prefix, string suffix) : base(fullName, shortName)
        {
            Name = fullName;
            if (!string.IsNullOrEmpty(shortName))
            {
                ShortName = shortName.Length > 50 ? shortName.Substring(0, 50) : shortName;
            }
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            AlternateNames = new Dictionary<string, string>(comparer);
            AlternateNames.TryAdd(ShortName, ShortName);
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
            return Name.CompareTo(other.Name)
                & ShortName.CompareTo(other.ShortName)
            ;
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return ShortName;
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
                return string.Equals(Name, obj.Name, StringComparison.Ordinal) &&
                    string.Equals(ShortName, obj.ShortName, StringComparison.Ordinal)
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
            return Name.GetHashCode()
                + ShortName.GetHashCode()
            ;
        }

        public ElementName CloneGeneric()
        {
            // Create a new instance and copy all relevant properties
            return new ElementName(
                fullName: this.Name,
                shortName: this.ShortName
            );
        }
        object ICloneable.Clone() => CloneGeneric();
    }
}

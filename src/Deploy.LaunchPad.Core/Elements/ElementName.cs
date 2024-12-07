using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Serialization;
using System;

namespace Deploy.LaunchPad.Core
{
    [Serializable]      
    [ComplexType]
    [DebuggerDisplay("{_debugDisplay}")]
    public partial class ElementName : IElementName
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
        [Required]
        [MaxLength(255, ErrorMessageResourceName = "Validation_ElementName_Full_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
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

        protected string _short = string.Empty;
        /// <summary>
        /// The short name of this element (if different from the FullName field). If not set, it will default to the first 20 characters of the full name.
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(24, ErrorMessageResourceName = "Validation_ElementName_Short_24CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Short
        {
            get
            {
                if (string.IsNullOrEmpty(_short))
                {
                    return Full;
                }
                else
                {
                    return _short;
                }
            }
            set
            {
                _short = value;
            }
        }

        protected string _prefix = string.Empty;
        /// <summary>
        /// The prefix, if any
        /// </summary>
        /// <value>The prefix of the element.</value>
        [MaxLength(12, ErrorMessageResourceName = "Validation_12CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Prefix
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


        protected string _suffix = string.Empty;
        /// <summary>
        /// The suffix, if any
        /// </summary>
        /// <value>The suffix of the element.</value>
        [MaxLength(12, ErrorMessageResourceName = "Validation_12CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Suffix
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


        protected ElementName()
        {
        }

        public ElementName(string fullName)
        {
            Full = fullName;
            if (!string.IsNullOrEmpty(fullName))
            {
                Short = fullName.Length > 24 ? fullName.Substring(0, 24) : fullName;
            }
        }

        public ElementName(string fullName, string shortName)
        {
            Full = fullName;
            if (!string.IsNullOrEmpty(shortName))
            {
                Short = shortName.Length > 24 ? shortName.Substring(0, 24) : shortName;
            }
        }


        public ElementName(string fullName, string shortName, string prefix, string suffix)
        {
            Full = fullName;
            if (!string.IsNullOrEmpty(shortName))
            {
                Short = shortName.Length > 24 ? shortName.Substring(0, 24) : shortName;
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
                return Full.Equals(obj.Full)
                    && Short.Equals(obj.Short)
                    && Prefix.Equals(obj.Prefix)
                    && Suffix.Equals(obj.Suffix)
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
            return Full.GetHashCode()
                + Short.GetHashCode()
                + Prefix.GetHashCode()
                + Suffix.GetHashCode()
            ;
        }
    }
}

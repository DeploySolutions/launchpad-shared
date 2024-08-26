using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    [Serializable]
    [ComplexType]
    public partial class ElementName : 
        IComparable<ElementName>, IEquatable<ElementName>
    {

        /// <summary>
        /// The name of this element
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; private set; }

        protected string _displayName;

        /// <summary>
        /// The display name of this object (if different from the Name field)
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayName))
                {
                    return Name;
                }
                else
                {
                    return _displayName;
                }
            }
            private set
            {
                _displayName = value;
            }
        }


        protected string _abbreviation;
        /// <summary>
        /// If this object does not have an abbreviation this will default to the first 10 characters of the Name.
        /// </summary>
        /// <value>The abbreviation of the element.</value>
        [MaxLength(12, ErrorMessageResourceName = "Validation_12CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Abbreviation
        {
            get
            {
                return _abbreviation;
            }
            private set
            {
                _abbreviation = value;
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
            private set
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
            private set
            {
                _suffix = value;
            }
        }


        private ElementName()
        {
        }

        public ElementName(string name)
        {
            Name = name;
            DisplayName = name;
            if (!String.IsNullOrEmpty(name))
            {
                Abbreviation = name.Length > 12 ? name.Substring(0, 12) : name;
            }
        }

        public ElementName(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
            if (!String.IsNullOrEmpty(name))
            {
                Abbreviation = name.Length > 12 ? name.Substring(0, 12) : name;
            }
        }


        public ElementName(string name, string displayName, string abbreviation)
        {
            Name = name;
            DisplayName = displayName;
            Abbreviation = abbreviation;
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
            return Name.CompareTo(other.Name) & DisplayName.CompareTo(other.DisplayName) & Abbreviation.CompareTo(other.Abbreviation);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return DisplayName;
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
                return Name.Equals(obj.Name) && DisplayName.Equals(obj.DisplayName) && Abbreviation.Equals(obj.Abbreviation);
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
                + DisplayName.GetHashCode()
                + Abbreviation.GetHashCode()
            ;
        }
    }
}

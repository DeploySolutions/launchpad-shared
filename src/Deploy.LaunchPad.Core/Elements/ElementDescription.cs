using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Diagnostics;
using System;

namespace Deploy.LaunchPad.Core
{
    [Serializable]
    [ComplexType]
    [DebuggerDisplay("{_debugDisplay}")]
    public partial class ElementDescription : IElementDescription
    {
        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        /// <value>The debug display.</value>
        protected virtual string _debugDisplay => $"{Full}.";

        protected string _full = string.Empty;
        /// <summary>
        /// The full description for this object
        /// </summary>
        /// <value>The description full.</value>
        [Required]
        [MaxLength(8096, ErrorMessageResourceName = "Validation_ElementDescription_Full_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
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
        /// A short description for this object. If not set, it will default to the first 255 characters of the full description.
        /// </summary>
        /// <value>The description short.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_ElementDescription_Short_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
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

        protected ElementDescription()
        {
        }

        public ElementDescription(string fullDescription)
        {
            Full = fullDescription;
            if (!string.IsNullOrEmpty(fullDescription))
            {
                Short = fullDescription.Length > 255 ? fullDescription.Substring(0, 255) : fullDescription;
            }
        }

        public ElementDescription(string fullDescription, string shortDescription)
        {
            Full = fullDescription;
            if (!string.IsNullOrEmpty(shortDescription))
            {
                Short = shortDescription.Length > 255 ? shortDescription.Substring(0, 255) : shortDescription;
            }
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(ElementDescription other)
        {
            return Full.CompareTo(other.Full) & Short.CompareTo(other.Short);
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
            if (obj != null && obj is ElementDescription)
            {
                return Equals(obj as ElementDescription);
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
        public virtual bool Equals(ElementDescription obj)
        {
            if (obj != null)
            {
                return Short.Equals(obj.Short) && Full.Equals(obj.Full);
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(ElementDescription x, ElementDescription y)
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
        public static bool operator !=(ElementDescription x, ElementDescription y)
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
            return Short.GetHashCode()
                + Full.GetHashCode();
        }

    }
}

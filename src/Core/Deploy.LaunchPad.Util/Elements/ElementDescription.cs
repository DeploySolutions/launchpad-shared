using Deploy.LaunchPad.Util.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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
    public partial class ElementDescription : ElementDescriptionLight, IElementDescription
    {

        protected string _shortDescription = string.Empty;
        /// <summary>
        /// A short description for this object. If not set, it will default to the first 255 characters of the full description.
        /// </summary>
        /// <value>The description short.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_ElementDescription_Short_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("short", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        [Column("core_description_short")]
        public virtual string ShortDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_shortDescription))
                {
                    return Description;
                }
                else
                {
                    return _shortDescription;
                }
            }
            set
            {
                _shortDescription = value;
            }
        }

        [SetsRequiredMembers]
        protected ElementDescription() :base()
        {
        }

        [SetsRequiredMembers]
        public ElementDescription(string fullDescription)
        {
            if (!string.IsNullOrEmpty(fullDescription))
            {
                Description = fullDescription;
                ShortDescription = fullDescription.Length > 255 ? fullDescription.Substring(0, 255) : fullDescription;
            }
        }

        [SetsRequiredMembers]
        public ElementDescription(string fullDescription, string shortDescription)
        {
            if (!string.IsNullOrEmpty(fullDescription))
            {
                Description = fullDescription;
            }
            if (!string.IsNullOrEmpty(shortDescription))
            {
                ShortDescription = shortDescription.Length > 255 ? shortDescription.Substring(0, 255) : shortDescription;
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
            return Description.CompareTo(other.Description) & ShortDescription.CompareTo(other.ShortDescription);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return Description;
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
                return ShortDescription.Equals(obj.ShortDescription) && Description.Equals(obj.Description);
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
            return ShortDescription.GetHashCode()
                + Description.GetHashCode();
        }

        public ElementDescription CloneGeneric()
        {
            // Create a new instance and copy all relevant properties
            return new ElementDescription(
                fullDescription: this.Description,
                shortDescription: this.ShortDescription
            );
        }
        object ICloneable.Clone() => CloneGeneric();
    }
}

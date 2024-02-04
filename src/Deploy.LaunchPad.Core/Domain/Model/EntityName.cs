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
    public partial class EntityName : 
        IComparable<EntityName>, IEquatable<EntityName>
    {

        /// <summary>
        /// The display name of this object
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; private set; }

        protected string _fullyQualifiedName;

        /// <summary>
        /// The fully-qualified name of this object (if different from the Name field)
        /// </summary>
        /// <value>The name of the fully qualified.</value>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string FullyQualifiedName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullyQualifiedName))
                {
                    return Name;
                }
                else
                {
                    return _fullyQualifiedName;
                }
            }
            private set
            {
                _fullyQualifiedName = value;
            }
        }

        private EntityName()
        {
        }

        public EntityName(string name)
        {
            Name = name;
            FullyQualifiedName = name;
        }

        public EntityName(string name, string fullyQualifiedName)
        {
            Name = name;
            FullyQualifiedName = fullyQualifiedName;
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(EntityName other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by FullyQualifiedName
            return Name.CompareTo(other.Name) & FullyQualifiedName.CompareTo(other.FullyQualifiedName);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return FullyQualifiedName;
        }


        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is EntityName)
            {
                return Equals(obj as EntityName);
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
        public virtual bool Equals(EntityName obj)
        {
            if (obj != null)
            {
                return Name.Equals(obj.Name) && FullyQualifiedName.Equals(obj.FullyQualifiedName);
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(EntityName x, EntityName y)
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
        public static bool operator !=(EntityName x, EntityName y)
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
                + FullyQualifiedName.GetHashCode();
        }
    }
}

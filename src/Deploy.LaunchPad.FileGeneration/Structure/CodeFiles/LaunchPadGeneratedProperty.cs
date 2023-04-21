﻿using System;
using System.Reflection;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a C# property generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedProperty : LaunchPadGeneratedMethodParameter,
        IComparable<LaunchPadGeneratedProperty>, IEquatable<LaunchPadGeneratedProperty>
    {
        public virtual PropertyAccessorLevelsEnum Scope { get; set; } = PropertyAccessorLevelsEnum.Public;

        public virtual bool IsReadonly { get; set; }

        public virtual bool IsVirtual { get; set; }

        public LaunchPadGeneratedProperty() : base()
        {
            IsReadonly = false;
            IsVirtual = false;
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadGeneratedProperty(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Scope = (PropertyAccessorLevelsEnum)info.GetValue("Scope", typeof(PropertyAccessorLevelsEnum));
            IsReadonly = info.GetBoolean("IsReadonly");
            IsVirtual = info.GetBoolean("IsVirtual");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Scope", Scope);
            info.AddValue("IsReadonly", IsReadonly);
            info.AddValue("IsVirtual", IsVirtual);
        }

        /// <summary>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other finite resources that 
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        public virtual void OnDeserialization(object sender)
        {
            // reconnect connection strings and other resources that won't be serialized
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="T">The source object to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        public override T Clone<T>()
        {
            T clone = new T();
            foreach (PropertyInfo info in GetType().GetProperties())
            {
                // ensure the property type is serializable
                if (info.GetType().IsSerializable)
                {
                    PropertyInfo cloneInfo = GetType().GetProperty(info.Name);
                    cloneInfo.SetValue(clone, info.GetValue(this, null), null);
                }
            }
            return clone;
        }

        public virtual LaunchPadGeneratedProperty CopyShallow()
        {
            return (LaunchPadGeneratedProperty)this.MemberwiseClone();
        }

        public virtual LaunchPadGeneratedProperty CopyDeep()
        {
            LaunchPadGeneratedProperty other = (LaunchPadGeneratedProperty)this.MemberwiseClone();
            return other;
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(LaunchPadGeneratedProperty other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by ObjectTypeFullName
            return ObjectTypeFullName.CompareTo(other.ObjectTypeFullName);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[LaunchPadGeneratedProperty: ");
            sb.Append(ToStringBaseProperties());
            sb.AppendFormat("Scope={0};", Scope);
            sb.AppendFormat("IsReadonly={0};", IsReadonly);
            sb.AppendFormat("IsVirtual={0};", IsVirtual);
            sb.Append(']');
            return sb.ToString();
        }


        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is LaunchPadGeneratedProperty)
            {
                return Equals(obj as LaunchPadGeneratedProperty);
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
        /// <returns></returns>
        public virtual bool Equals(LaunchPadGeneratedProperty obj)
        {
            if (obj != null)
            {

                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Id.Equals(obj.Id) && IdType.Equals(obj.IdType) && ObjectTypeAssemblyName.Equals(obj.ObjectTypeAssemblyName)
                    && ObjectTypeFullName.Equals(obj.ObjectTypeFullName)
                    && ObjectTypeName.Equals(obj.ObjectTypeName);

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(LaunchPadGeneratedProperty x, LaunchPadGeneratedProperty y)
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
        public static bool operator !=(LaunchPadGeneratedProperty x, LaunchPadGeneratedProperty y)
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
        public virtual int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

}

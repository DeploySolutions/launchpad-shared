using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The base class containing properties for all LaunchPad RAD file generation processes.
    /// This is the top level element in the LaunchPad Generated object hierarchy. 
    /// </summary>    
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract partial class LaunchPadGeneratedObjectBase : ILaunchPadGeneratedObject,
        IComparable<LaunchPadGeneratedObjectBase>, IEquatable<LaunchPadGeneratedObjectBase>
    {
        /// <summary>
        /// The unique id of the object (if present)
        /// </summary>
        [JsonProperty("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// The singular name of the object 
        /// </summary>
        [JsonProperty("name")]
        public virtual string Name { get; set; } = string.Empty;


        /// <summary>
        /// The abbreviation of the object (if any)
        /// </summary>
        [JsonConverter(typeof(LocalizedJsonConverter<string>))]
        public virtual string Abbreviation { get; set; } = string.Empty;

        /// <summary>
        /// The prefix to apply to the name (if any).
        /// </summary>
        public virtual string NamePrefix { get; set; } = string.Empty;

        /// <summary>
        /// The suffix to apply to the name (if any).
        /// </summary>
        public virtual string NameSuffix { get; set; } = string.Empty;


        /// <summary>
        /// The description of the object
        /// </summary>
        [JsonProperty("description")]
        public virtual string Description { get; set; } = string.Empty;

        /// <summary>
        /// Code annotations for the object
        /// </summary>
        public virtual string Annotations { get; set; } = string.Empty;

        /// <summary>
        /// The C# type of this object
        /// </summary>
        public virtual string ObjectTypeName { get; set; } = string.Empty;

        /// <summary>
        /// The C# full type name of this object
        /// </summary>
        public virtual string ObjectTypeFullName { get; set; } = string.Empty;

        /// <summary>
        /// The assembly name in which this C# object is defined
        /// </summary>
        public virtual string ObjectTypeAssemblyName { get; set; } = string.Empty;

        /// <summary>
        /// The C# type of this object's id.
        /// </summary>
        public virtual string IdType { get; set; } = "System.Int32";


        public LaunchPadGeneratedObjectBase() : base()
        {
            ObjectTypeName = this.GetType().Name;
            ObjectTypeFullName = this.GetType().FullName;
            ObjectTypeAssemblyName = this.GetType().Assembly.FullName;
            IdType = string.Empty;
            Id = string.Empty;
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadGeneratedObjectBase(SerializationInfo info, StreamingContext context) 
        {
            Id = info.GetString("Id");
            IdType = info.GetString("IdType");
            ObjectTypeName = info.GetString("ObjectTypeName");
            ObjectTypeFullName = info.GetString("ObjectTypeFullName");
            ObjectTypeAssemblyName = info.GetString("ObjectTypeAssemblyName");

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("IdType", IdType);
            info.AddValue("ObjectTypeName", ObjectTypeName);
            info.AddValue("ObjectTypeFullName", ObjectTypeFullName);
            info.AddValue("ObjectTypeAssemblyName", ObjectTypeAssemblyName);

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
        public virtual T Clone<T>() where T : ILaunchPadGeneratedObject, new()
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

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(LaunchPadGeneratedObjectBase other)
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
            sb.Append("[DomainEntityBase: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected virtual String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("IdType={0};", IdType);
            sb.AppendFormat("ObjectTypeName={0};", ObjectTypeName);
            sb.AppendFormat("ObjectTypeFullName={0};", ObjectTypeFullName);
            sb.AppendFormat("ObjectTypeAssemblyName={0};", ObjectTypeAssemblyName);
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is LaunchPadGeneratedObjectBase)
            {
                return Equals(obj as LaunchPadGeneratedObjectBase);
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
        public virtual bool Equals(LaunchPadGeneratedObjectBase obj)
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
        public static bool operator ==(LaunchPadGeneratedObjectBase x, LaunchPadGeneratedObjectBase y)
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
        public static bool operator !=(LaunchPadGeneratedObjectBase x, LaunchPadGeneratedObjectBase y)
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

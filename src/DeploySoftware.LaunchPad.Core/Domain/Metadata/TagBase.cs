//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

namespace DeploySoftware.LaunchPad.Core.Domain
{
    using JetBrains.Annotations;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;


    /// <summary>
    /// Base class for Metadata Tags. Implements <see cref="ILaunchPadMetadataTag">ILaunchPadMetadataTag</see> and provides
    /// base functionality for many of its methods. Inherits from ASP.NET Boilerplate's IEntity interface.
    /// </summary>
    public abstract partial class TagBase : 
        ILaunchPadMetadataTag,
        IComparable<TagBase>, IEquatable<TagBase>    
    {


        /// <summary>
        /// The unique id of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual long Id { get; set; }

        /// <summary>
        /// The key (name) of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Key { get; set; }

        /// <summary>
        /// The value of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Value { get; set; }

        /// <summary>
        /// The scheme of this metadata tag, if any
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Schema { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="TagBase">Tag</see> class
        /// </summary>
        protected TagBase()
        {
            Key = String.Empty;
            Value = String.Empty;
            Schema = String.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TagBase">TagBase</see> class given a key, and some value. 
        /// </summary>
        /// <param name="key">The unique identifier for this tag</param>
        /// <param name="value">The desired value for this tag</param>
        protected TagBase(String key, String value)
        {
            Key = key;
            Value = value;
            Schema = String.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TagBase">Tag</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="key">The key for this tag</param>
        /// <param name="value">The desired value for this tag</param>
        ///  <param name="schema">The desired schema for this tag</param>
        protected TagBase(String key, String value, string schema)
        {
            Key = key;
            Value = value;
            Schema = schema;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected TagBase(SerializationInfo info, StreamingContext context)
        {
            Key = info.GetString("Key");
            Value = info.GetString("Value");
            Schema = info.GetString("Schema");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Key", Key);
            info.AddValue("Value", Value);
            info.AddValue("Schema", Schema);

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
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected virtual TEntity Clone<TEntity>() where TEntity : ILaunchPadMetadataTag, new()
        {
            TEntity clone = new TEntity();
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
        public virtual int CompareTo(ILaunchPadMetadataTag other)
        {
             int result;
            result = Key.CompareTo(other.Key);
            if (result == 0)
                result = Value.CompareTo(other.Value);
            if (result == 0)
                result = Schema.CompareTo(other.Schema);
            return result;
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Tag: ");
            sb.Append(Key);
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
            sb.AppendFormat("Id={0};", Id); 
            sb.AppendFormat("Key={0};", Key);
            sb.AppendFormat("Value={0};", Value);
            sb.AppendFormat("Scheme={0};", Schema);
            return sb.ToString();
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(TagBase other)
        {
            int result;
            result = Key.CompareTo(other.Key);
            if (result ==0)
                result = Value.CompareTo(other.Value);
            if (result == 0)
                result = Schema.CompareTo(other.Schema);
            return result;
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is TagBase)
            {
                return Equals(obj as TagBase);
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
        public virtual bool Equals(TagBase obj)
        {
            if (obj != null)
            {
                return Id.Equals(obj.Id) && Key.Equals(obj.Key) && Value.Equals(obj.Value) && Schema.Equals(obj.Schema); 
            }
            return false;
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return 
                Id.GetHashCode() +
                Key.GetHashCode() + 
                Value.GetHashCode() +
                Schema.GetHashCode()
           ;
        }

    }
}

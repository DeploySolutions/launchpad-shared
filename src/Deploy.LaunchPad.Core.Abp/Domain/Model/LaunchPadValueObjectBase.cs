﻿//LaunchPad Shared
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

using Abp.Domain.Values;
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain.Model
{

    /// <summary>
    /// Base class for transient / value objects, ie. those that are not Domain Entities.
    /// By definition this value object has no specific identity, and is transient / not persisted. 
    /// Implements <see cref="IValueObject">IValueObject</see> and inherits from ABP's ValueObject base class, which provides
    /// base functionality for many of its methods. Inherits from ABP's ValueObject class.
    /// Implements AspNetBoilerplate's auditing interfaces.
    /// </summary>
    [Serializable]
    public abstract partial class LaunchPadValueObjectBase : ValueObject,
        ILaunchPadValueObject
    {

        /// <summary>
        /// If this object is a regular domain entity, an aggregate root, or an aggregate child
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DomainEntityType EntityType { get; } = DomainEntityType.ValueObject;


        /// <summary>  
        /// Initializes a new instance of the <see cref="ValueObjectBase">ValueObject</see> class
        /// </summary>
        public LaunchPadValueObjectBase() : base()
        {


        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadValueObjectBase(SerializationInfo info, StreamingContext context)
        {

        }


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {


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
        /// <typeparam name="TValueObject">The source ValueObject to clone</typeparam>
        /// <returns>A shallow clone of the ValueObject and its serializable properties</returns>
        protected virtual TValueObject Clone<TValueObject>() where TValueObject : ILaunchPadValueObject, new()
        {
            TValueObject clone = new TValueObject();
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


    }
}

//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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
    using Castle.MicroKernel.ModelBuilder.Descriptors;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Xml.Serialization;

    /// <summary>
    /// The base class for all Keys used by the entities and files in the LaunchPad platform 
    /// </summary>
    /// <typeparam name="TUniqueId">The Type of the Id field</typeparam>
    public abstract class KeyBase<TIdType> : IKey<TIdType>
    {
        #region IKey Members

        /// <summary>
        /// The globally unique identifier that uniquely identifies this object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual TIdType Name { get; set; }

        #endregion

        protected KeyBase()
        {

        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="DataKeyBase{TUniqueId}">KeyBase{TUniqueId}</see> class
        /// with the given name
        /// </summary>
        protected KeyBase(TIdType name)
        {
            Name = name;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected KeyBase(SerializationInfo info, StreamingContext context)
        {
            Name = (TIdType)info.GetValue("Name", typeof(TIdType));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name); 
        }
    }
}

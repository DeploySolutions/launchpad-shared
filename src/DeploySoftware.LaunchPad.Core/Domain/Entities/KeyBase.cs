﻿//LaunchPad Shared
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
        public virtual TIdType LaunchPadId { get; set; }

        /// <summary>
        /// The ISO Culture code of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual String Culture { get; set; }

        #endregion

        /// <summary>
        /// A convenience readonly property to get a <see cref="CultureInfo">CultureInfo</see> instance from the current 
        /// culture code
        /// </summary>
        public CultureInfo GetCultureInfo { get { return new CultureInfo(Culture); } }

        /// <summary>  
        /// Initializes a new instance of the <see cref="DataKeyBase{TUniqueId}">KeyBase{TUniqueId}</see> class. 
        /// The culture code will default to the application's DefaultCultureName setting. 
        /// </summary>
        protected KeyBase()
        {
            //TODO: set up default settings from dependency injection
            //CultureName = IApplicationState.Get<IApplicationContext>("ApplicationContext").Settings.DefaultCultureName;
            Culture = "en";
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="DataKeyBase{TUniqueId}">KeyBase{TUniqueId}</see> class
        /// with the given culture code.  
        /// </summary>
        /// <param name="cultureName">The culture code of the key</param>
        protected KeyBase(TIdType id, String cultureName)
        {
            LaunchPadId = id;
            Culture = cultureName;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected KeyBase(SerializationInfo info, StreamingContext context)
        {
            LaunchPadId = (TIdType)info.GetValue("LaunchPadId", typeof(TIdType));
            Culture = info.GetString("Culture");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LaunchPadId", LaunchPadId); 
            info.AddValue("Culture", Culture);
        }
    }
}
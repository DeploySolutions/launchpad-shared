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

using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    /// <summary>
    /// Represents the base properties a LaunchPad Data Transfer Object would show in a list.
    /// Of course subclassing DTOs will contain additional properties.
    /// </summary>
    /// <typeparam name="TEntityType">The type of the Id</typeparam>
    public abstract partial class ListResultDtoBase<TEntityType> : ListResultDto<TEntityType>, IHasTotalCount, ICanBeAppServiceMethodOutput        
    {
        /// <summary>
        /// The total Count of the items contained in this list.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public int TotalCount { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>

        public ListResultDtoBase() : base()
        {

        }

        /// <summary>
        /// Default constructor where the tenant id is known
        /// </summary>
        public ListResultDtoBase(IReadOnlyList<TEntityType> items) : base(items)
        {

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ListResultDtoBase(SerializationInfo info, StreamingContext context) 
        {
            Items = (IReadOnlyList<TEntityType>)info.GetValue("Items", typeof(IReadOnlyList<TEntityType>));
            TotalCount = info.GetInt32("TotalCount");
        }


        #endregion


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Items", Items);
            info.AddValue("TotalCount", TotalCount);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ListResultDtoBase : ");
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
            //
            // ABP properties
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected TEntity Clone<TEntity>() where TEntity : ListResultDtoBase<TEntityType>, new()
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

    }
}

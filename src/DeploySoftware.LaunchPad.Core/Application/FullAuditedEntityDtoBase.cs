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
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// Represents a Data Transfer Object that is fully audited.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract class FullAuditedEntityDtoBase<TIdType> : EntityDtoBase<TIdType>,
        IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, IDeletionAudited,
        ISoftDelete, IPassivable,
        IComparable<EntityDtoBase<TIdType>>, IEquatable<EntityDtoBase<TIdType>>
    {


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="tenantId"></param>
        protected FullAuditedEntityDtoBase() : base()
        {

        }

        /// <summary>
        /// Default constructor where the tenant id is known
        /// </summary>
        /// <param name="tenantId"></param>
        public FullAuditedEntityDtoBase(int? tenantId) : base(tenantId)
        {

        }

        public FullAuditedEntityDtoBase(int? tenantId, TIdType id) : base(tenantId, id)
        {
           
        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FullAuditedEntityDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }

#endregion


       
    }
}

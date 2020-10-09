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
using System.Runtime.Serialization;
using System.Security.Permissions;


namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// Represents the detail properties that may be used in order to filter an entity
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class FilterDetailDtoBase<TIdType> : EntityDetailDtoBase<TIdType>, IPagedResultRequest
    {


        public string Sort { get; set; }

        public int SkipCount { get; set; }

        public int MaxResultCount { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected FilterDetailDtoBase() : base()
        {
           
        }

#endregion


    }
}

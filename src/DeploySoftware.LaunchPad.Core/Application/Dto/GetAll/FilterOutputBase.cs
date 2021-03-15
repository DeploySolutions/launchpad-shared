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


using DeploySoftware.LaunchPad.Core.Domain;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    /// <summary>
    /// Represents the minimal properties that may be used in order to filter an entity in a list
    /// </summary>
    public abstract partial class FilterOutputBase<TEntityType,TIdType>
        where TEntityType: DomainEntityBase<TIdType>
    {
        public IEnumerable<TEntityType> Filter { get; set; }
        public int TotalCount { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public FilterOutputBase() : base()
        {
           
        }

#endregion


    }
}

//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 
//This file is a derivative work from the original created in NCommon and copyright 2010 by Ritesh Rao 

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DeploySoftware.LaunchPad.Common.Domain.Events
{ 
    /// <summary>
    /// Interface used by handlers of domain events.
    /// </summary>
    /// <typeparam name="T">A type of <see cref="DomainEvent"/></typeparam>
    public interface Handles<T> where T : IDomainEvent
    {
        /// <summary>
        /// Method invoked when a domain event of <typeparamref name="T"/> is raised.
        /// </summary>
        /// <param name="event"></param>
        void Handle(T @event);
    }
}
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

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    ///<summary>
    /// DomainEvent class that allowes raising domain events from domain entities and allow registring
    /// custom callbacks to execute when a <see cref="IDomainEvent"/> is raised.
    ///</summary>
    public class DomainEvent
    {
        const string CallbackListKey = "DomainEvent.Callbacks";

        ///<summary>
        /// Registers a callback to be called when a domain event is raised.
        ///</summary>
        ///<param name="callback">An <see cref="Action{T}"/> to be invoked.</param>
        ///<typeparam name="T">The domain event that the callback is registered to handle.</typeparam>
        public static void RegisterCallback<T>(Action<T> callback) where T : IDomainEvent
        {
            // TODO: register callbacks code
            // https://lostechies.com/jimmybogard/2010/08/04/container-friendly-domain-events/ 
            // https://ayende.com/blog/890/the-ultimate-disposable
        }

        ///<summary>
        /// Clears all callbacks registered on the current thread.
        ///</summary>
        public static void ClearCallbacks()
        {
            // TODO: clear callbacks code
        }

        ///<summary>
        /// Raises a <see cref="IDomainEvent"/>.
        ///</summary>
        ///<param name="event">A instance <see cref="IDomainEvent"/> to raise.</param>
        ///<typeparam name="T">A type implementing <see cref="IDomainEvent"/></typeparam>
        public static void Raise<T>(T @event) where T : IDomainEvent
        {
            // TODO: Raise code
        }
    }
}
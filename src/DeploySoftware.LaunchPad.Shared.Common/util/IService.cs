//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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
using System.Text;

namespace DeploySoftware.LaunchPad.Common.Util
{
    /// <summary>
    /// This is a marker interface to support auto-registration as a IoC / Dependency Injection component.
    /// By convention, any interface under a namespace ending with "Services" will be registered upon application startup.
    /// However, some component services may wish to autoregister while under another namespace.
    /// Implementing the IService interface will ensure they are auto-registered. 
    /// </summary>
    public interface IService
    {
    }
}

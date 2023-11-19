// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ILaunchPadDomainService.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

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

using Abp.Domain.Services;

namespace Deploy.LaunchPad.Core.Abp.Domain.Model
{
    /// <summary>
    /// This is a marker interface to support auto-registration as a IoC / Dependency Injection component.
    /// By convention, any interface under a namespace ending with "Services" will be registered upon application startup.
    /// However, some component services may wish to autoregister while under another namespace.
    /// Implementing the ILaunchPadService interface will ensure they are auto-registered.
    /// </summary>
    public partial interface ILaunchPadDomainService : IDomainService
    {
    }
}

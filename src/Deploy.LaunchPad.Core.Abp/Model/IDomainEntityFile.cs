// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IFile.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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

namespace Deploy.LaunchPad.Core.Abp.Model
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Core.Abp.Model;
    using Deploy.LaunchPad.Files;
    using Deploy.LaunchPad.Util;
    using Deploy.LaunchPad.Files;

    /// <summary>
    /// Marks any object as a file that can be manipulated by the platform AND tracked as a domain entity.
    /// If you just want to manipulate files in the regular manner, without tracking them as domain entities, use IFile.
    /// Each file is uniquely identified by its id, which could be a complex name or some other unique property like a GUID or integer.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    public partial interface IDomainEntityFile<TIdType, TFileContentType, TSchemaFormat> : ILaunchPadDomainEntity<TIdType>,
        IFile<TFileContentType, TSchemaFormat>
    {


    }
}

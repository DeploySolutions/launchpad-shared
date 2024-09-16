// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="VolFile.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Space.Satellites.Core
{
    using Deploy.LaunchPad.Core.Abp.Domain;

    /// <summary>
    /// Class VolFile.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Domain.FileBase{TPrimaryKey, System.Byte[]}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.FileBase{TPrimaryKey, System.Byte[]}" />
    public partial class VolFile<TPrimaryKey> : FileBase<TPrimaryKey, byte[]>
    {
        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public new string Extension
        {
            get { return ".vol"; }
        }

    }
}

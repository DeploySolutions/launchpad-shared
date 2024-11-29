﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="TifFile.cs" company="Deploy Software Solutions, inc.">
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


using Deploy.LaunchPad.Core.Abp.Domain.Model;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Class TifFile.
    /// Implements the <see cref="Model.DomainEntityFileBase{TIdType, System.Byte[]}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Model.DomainEntityFileBase{TIdType, System.Byte[]}" />
    public partial class TifFile<TIdType> : DomainEntityFileBase<TIdType, byte[]>
    {
        /// <summary>
        /// The extension of the file
        /// </summary>
        /// <value>The extension.</value>
        public override string Extension => ".tif";

        /// <summary>
        /// Initializes a new instance of the <see cref="TifFile{TIdType}"/> class.
        /// </summary>
        public TifFile() : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TifFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public TifFile(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TifFile{TIdType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        public TifFile(TIdType id, string fileName) : base(id, fileName)
        {

        }
    }
}

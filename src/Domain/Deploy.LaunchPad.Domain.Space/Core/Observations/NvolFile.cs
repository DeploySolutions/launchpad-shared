// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Domain.Space
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="NvolFile.cs" company="Deploy Software Solutions, inc.">
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



using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Files;

namespace Deploy.LaunchPad.Domain.Space.Core
{


    /// <summary>
    /// Class NvolFile.
    /// Implements the <see cref="Deploy.LaunchPad.Files.FileBase{System.Byte[]}" />
    /// <seealso cref="Deploy.LaunchPad.Files.DomainEntityFileBase{System.Byte[]}" />
    /// </summary>
    public partial class NvolFile : FileBase<byte[], NvolSchemaFormat>
    {
        /// <summary>
        /// The extension of the file
        /// </summary>
        /// <value>The extension.</value>
        public override string Extension
        {
            get { return ".nvol"; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        public NvolFile(string fileName) : base(fileName)
        {
            Name = fileName;
        }

    }
}

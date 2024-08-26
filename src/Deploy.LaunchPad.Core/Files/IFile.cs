// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IFile.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Domain
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Core.Domain.Model;
    using Deploy.LaunchPad.Core.Util;

    /// <summary>
    /// Marks any object as a file that can be manipulated by the platform.
    /// Each file is uniquely identified by its id, which could be a complex name or some other unique property like a GUID or integer.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    public partial interface IFile<TIdType, TFileContentType> : ILaunchPadObject
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// <font color="red">Badly formed XML comment.</font>
        public TIdType Id { get; set; }

        /// <summary>
        /// The size of the file, in bytes
        /// </summary>
        /// <value>The size.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public Int64 Size { get; set; }

        /// <summary>
        /// The name of the file (if different from its ID)
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public ElementName Name { get; set; }

        /// <summary>
        /// The extension of the file
        /// </summary>
        /// <value>The extension.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String Extension { get; set; }

        /// <summary>
        /// The content / mime type of the file
        /// </summary>
        /// <value>The type of the MIME.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public String MimeType { get; set; }


        /// <summary>
        /// The content of the file
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public TFileContentType Content { get; set; }

        /// <summary>
        /// Properties and methods for the file's content hash (to facilitate file verification)
        /// </summary>
        /// <value>The checksum.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public Checksum Checksum { get; set; }


    }
}

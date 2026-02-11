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

namespace Deploy.LaunchPad.Files
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Files.Formats;

    using Deploy.LaunchPad.Util.Elements;
    using Deploy.LaunchPad.Util.Metadata;

    /// <summary>
    /// Marks any object as a file that can be manipulated by the platform.
    /// </summary>
    /// <typeparam name="TFileContentType">The type of the file content.</typeparam>
    public partial interface IFile<TFileContentType, TSchemaFormat> : 
        ILaunchPadObject, 
        ILaunchPadMinimalProperties,
        IMustHaveCreationTimestamp,
        IMayHaveLastModificationTimestamp
    {
       
        /// <summary>
        /// The size of the file, in bytes
        /// </summary>
        /// <value>The size.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public Int64 Size { get; set; }

        /// <summary>
        /// The extension of the file
        /// </summary>
        /// <value>The extension.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Extension { get; set; }

        /// <summary>
        /// The encoding of the file
        /// </summary>
        /// <value>The encoding.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Encoding { get; set; }

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
        /// The schema of the file
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public ILaunchPadSchemaDetails<TSchemaFormat>? Schema { get; set; }




    }
}

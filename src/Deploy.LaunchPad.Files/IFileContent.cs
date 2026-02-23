// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IFileContent.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// Interface IFileContent
    /// </summary>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    public partial interface IFileContent<TFileContentType>
    {
        /// <summary>
        /// Get the content/data of the file
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public TFileContentType Content { get; set; }
        public string Encoding { get; set; }
    }
}

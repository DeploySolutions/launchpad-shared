// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IPythonInstallation.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Interface IPythonInstallation
    /// Extends the <see cref="ILaunchPadObject" />
    /// Extends the <see cref="ISerializable" />
    /// Extends the <see cref="System.IComparable{Deploy.LaunchPad.Python.PythonInstallation}" />
    /// Extends the <see cref="System.IEquatable{Deploy.LaunchPad.Python.PythonInstallation}" />
    /// </summary>
    /// <seealso cref="ILaunchPadObject" />
    /// <seealso cref="ISerializable" />
    /// <seealso cref="System.IComparable{Deploy.LaunchPad.Python.PythonInstallation}" />
    /// <seealso cref="System.IEquatable{Deploy.LaunchPad.Python.PythonInstallation}" />
    public partial interface IPythonInstallation : ILaunchPadObject, ISerializable, IComparable<PythonInstallation>, IEquatable<PythonInstallation>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description short.
        /// </summary>
        /// <value>The description short.</value>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        string DescriptionShort { get; set; }

        /// <summary>
        /// Gets or sets the description full.
        /// </summary>
        /// <value>The description full.</value>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        string? DescriptionFull { get; set; }

        /// <summary>
        /// Gets or sets the install location.
        /// </summary>
        /// <value>The install location.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        Uri InstallLocation { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        IPythonVersion Version { get; set; }

        /// <summary>
        /// Gets or sets the module locations.
        /// </summary>
        /// <value>The module locations.</value>
        [DataObjectField(false)]
        [XmlElement]
        IDictionary<string, Uri> ModuleLocations { get; set; }
    }
}
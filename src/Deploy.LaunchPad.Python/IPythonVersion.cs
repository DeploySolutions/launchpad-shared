// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-11-2023
// ***********************************************************************
// <copyright file="IPythonVersion.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Python
{

    /// <summary>
    /// Interface IPythonVersion
    /// Extends the <see cref="ISerializable" />
    /// Extends the <see cref="System.IComparable{Deploy.LaunchPad.Python.PythonVersion}" />
    /// Extends the <see cref="System.IEquatable{Deploy.LaunchPad.Python.PythonVersion}" />
    /// </summary>
    /// <seealso cref="ISerializable" />
    /// <seealso cref="System.IComparable{Deploy.LaunchPad.Python.PythonVersion}" />
    /// <seealso cref="System.IEquatable{Deploy.LaunchPad.Python.PythonVersion}" />
    public partial interface IPythonVersion:
        ISerializable,
        IComparable<PythonVersion>,
        IEquatable<PythonVersion>
    {
        /// <summary>
        /// Gets the major version.
        /// </summary>
        /// <value>The major version.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        PythonMajorVersion MajorVersion { get;  }

        /// <summary>
        /// Gets the minor version.
        /// </summary>
        /// <value>The minor version.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        PythonMinorVersion MinorVersion { get;  }

        /// <summary>
        /// Gets the patch version.
        /// </summary>
        /// <value>The patch version.</value>
        [DataObjectField(false)]
        [XmlElement]
        int PatchVersion { get; }

        /// <summary>
        /// Gets the first released.
        /// </summary>
        /// <value>The first released.</value>
        [DataObjectField(false)]
        [XmlElement]
        DateTime FirstReleased { get; }

        /// <summary>
        /// Gets the end of support.
        /// </summary>
        /// <value>The end of support.</value>
        [DataObjectField(false)]
        [XmlElement]
        DateTime EndOfSupport { get; }

        /// <summary>
        /// Gets the maintenance status.
        /// </summary>
        /// <value>The maintenance status.</value>
        [DataObjectField(false)]
        [XmlElement]
        PythonMaintenanceStatus MaintenanceStatus { get; }

        /// <summary>
        /// Gets the release notes.
        /// </summary>
        /// <value>The release notes.</value>
        [DataObjectField(false)]
        [XmlElement]
        Uri ReleaseNotes { get;  }


        /// <summary>
        /// Gets the download.
        /// </summary>
        /// <value>The download.</value>
        [DataObjectField(false)]
        [XmlElement]
        Uri Download { get; }

        /// <summary>
        /// Returns the major, minor, and patch version number
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetFullVersion();

        /// <summary>
        /// Returns the major and minor version number only
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetShortVersion();
    }
}

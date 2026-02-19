// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IPythonInstallationReference.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Interface IPythonInstallationReference
    /// Extends the <see cref="ILaunchPadObject" />
    /// Extends the <see cref="ISerializable" />
    /// Extends the <see cref="System.IComparable{Deploy.LaunchPad.Python.PythonInstallationReference}" />
    /// Extends the <see cref="System.IEquatable{Deploy.LaunchPad.Python.PythonInstallationReference}" />
    /// </summary>
    /// <seealso cref="ILaunchPadObject" />
    /// <seealso cref="ISerializable" />
    /// <seealso cref="System.IComparable{Deploy.LaunchPad.Python.PythonInstallationReference}" />
    /// <seealso cref="System.IEquatable{Deploy.LaunchPad.Python.PythonInstallationReference}" />
    public partial interface IPythonInstallationReference : ILaunchPadObject, ISerializable, IComparable<PythonInstallationReference>, IEquatable<PythonInstallationReference>
    {
        /// <summary>
        /// The ID of the Python Installation this reference points to
        /// </summary>
        /// <value>The installation identifier.</value>
        [Required]
        [DataObjectField(true)]
        [XmlAttribute]
        public string InstallationId { get; set; }

        /// <summary>
        /// Gets the python installation from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IPythonInstallation.</returns>
        public IPythonInstallation GetPythonInstallationFromId(string id);

    }
}

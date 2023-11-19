// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-14-2023
// ***********************************************************************
// <copyright file="PythonInstallationJson.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Python.Json
{
    /// <summary>
    /// Class PythonInstallationJson.
    /// </summary>
    [Serializable()]
    public partial class PythonInstallationJson
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description short.
        /// </summary>
        /// <value>The description short.</value>
        public virtual string DescriptionShort { get; set; }

        /// <summary>
        /// Gets or sets the description full.
        /// </summary>
        /// <value>The description full.</value>
        public virtual string? DescriptionFull { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual PythonVersionJson Version { get; set; }

        /// <summary>
        /// Gets or sets the install location.
        /// </summary>
        /// <value>The install location.</value>
        public virtual Uri InstallLocation { get; set; }

        /// <summary>
        /// Gets or sets the module locations.
        /// </summary>
        /// <value>The module locations.</value>
        public virtual IDictionary<string, Uri> ModuleLocations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallationJson"/> class.
        /// </summary>
        public PythonInstallationJson() : base()
        {
            
        }
    }

}
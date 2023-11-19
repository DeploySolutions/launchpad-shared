// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-14-2023
// ***********************************************************************
// <copyright file="PythonVersionJson.cs" company="Deploy Software Solutions, inc.">
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
    /// Class PythonVersionJson.
    /// </summary>
    [Serializable()]
    public partial class PythonVersionJson
    {
        /// <summary>
        /// Gets the major.
        /// </summary>
        /// <value>The major.</value>
        public virtual int Major { get; private set; }

        /// <summary>
        /// Gets the minor.
        /// </summary>
        /// <value>The minor.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual int Minor { get; private set; }

        /// <summary>
        /// Gets or sets the patch.
        /// </summary>
        /// <value>The patch.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual int Patch { get; set; } = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="PythonVersionJson"/> class.
        /// </summary>
        public PythonVersionJson()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonVersionJson"/> class.
        /// </summary>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="patchVersion">The patch version.</param>
        public PythonVersionJson(int majorVersion, int minorVersion, int patchVersion)
        {
            Major = majorVersion;
            Minor = minorVersion;
            Patch = patchVersion;
        }
    }
}
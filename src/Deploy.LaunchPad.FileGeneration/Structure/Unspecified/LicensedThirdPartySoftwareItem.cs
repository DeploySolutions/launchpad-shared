// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-26-2023
// ***********************************************************************
// <copyright file="LicensedThirdPartySoftwareItem.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// This class represents a licensed third party or open source item (element, file, component, library, assembly, etc).
    /// It allows us to provide appropriate usage and credit information while generating code.
    /// </summary>
    [Serializable]
    public partial class LicensedThirdPartySoftwareItem : ILicensedThirdPartySoftwareItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the legal.
        /// </summary>
        /// <value>The name of the legal.</value>
        public virtual string LegalName { get; set; }


        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; } = "";


        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        /// <value>The copyright.</value>
        public virtual string Copyright { get; set; } = "";

        /// <summary>
        /// Gets or sets the source repository.
        /// </summary>
        /// <value>The source repository.</value>
        public virtual SourceControlRepository SourceRepository { get; set; }

        /// <summary>
        /// Gets or sets the usage rights.
        /// </summary>
        /// <value>The usage rights.</value>
        public virtual UsageRights UsageRights { get; set; }

        /// <summary>
        /// Gets or sets the more information URI.
        /// </summary>
        /// <value>The more information URI.</value>
        public virtual Uri MoreInformationUri { get; set; }

        /// <summary>
        /// Gets or sets the refresh date.
        /// </summary>
        /// <value>The refresh date.</value>
        public virtual DateTime RefreshDate { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensedThirdPartySoftwareItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LicensedThirdPartySoftwareItem(string name)
        {
            Name = name;
            LegalName = name;
            SourceRepository = new SourceControlRepository();
            UsageRights = new UsageRights();
            RefreshDate = DateTime.Today;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LicensedThirdPartySoftwareItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        public LicensedThirdPartySoftwareItem(string name, string version)
        {
            Name = name;
            LegalName = name;
            SourceRepository = new SourceControlRepository();
            UsageRights = new UsageRights();
            Version = version;
            RefreshDate = DateTime.Today;
        }


    }
}

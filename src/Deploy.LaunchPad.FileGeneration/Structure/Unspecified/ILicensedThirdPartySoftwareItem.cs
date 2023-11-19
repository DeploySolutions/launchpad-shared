// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-26-2023
// ***********************************************************************
// <copyright file="ILicensedThirdPartySoftwareItem.cs" company="Deploy Software Solutions, inc.">
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
    /// Interface ILicensedThirdPartySoftwareItem
    /// </summary>
    public partial interface ILicensedThirdPartySoftwareItem
    {
        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        /// <value>The copyright.</value>
        string Copyright { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }
        /// <summary>
        /// Gets or sets the name of the legal.
        /// </summary>
        /// <value>The name of the legal.</value>
        string LegalName { get; set; }
        /// <summary>
        /// Gets or sets the more information URI.
        /// </summary>
        /// <value>The more information URI.</value>
        Uri MoreInformationUri { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the source repository.
        /// </summary>
        /// <value>The source repository.</value>
        SourceControlRepository SourceRepository { get; set; }
        /// <summary>
        /// Gets or sets the usage rights.
        /// </summary>
        /// <value>The usage rights.</value>
        UsageRights UsageRights { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; set; }

        /// <summary>
        /// Gets or sets the refresh date.
        /// </summary>
        /// <value>The refresh date.</value>
        DateTime RefreshDate { get; set; }
    }
}
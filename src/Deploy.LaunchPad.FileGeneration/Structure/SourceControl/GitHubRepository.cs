﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="GitHubRepository.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    /// <summary>
    /// Class GitHubRepository.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.SourceControl.GitRepository" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.SourceControl.GitRepository" />
    [Serializable]
    public partial class GitHubRepository : GitRepository
    {

        /// <summary>
        /// Gets or sets the org.
        /// </summary>
        /// <value>The org.</value>
        public virtual string Org { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the releases.
        /// </summary>
        /// <value>The releases.</value>
        public virtual IDictionary<string, IRelease> Releases { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubRepository"/> class.
        /// </summary>
        public GitHubRepository() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Releases = new Dictionary<string, IRelease>(comparer);
        }
    }
}

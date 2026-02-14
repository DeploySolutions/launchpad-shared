// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="GitRepository.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.SourceControl
{
    /// <summary>
    /// Class GitRepository.
    /// Implements the <see cref="Deploy.LaunchPad.Core.SourceControl.SourceControlRepository" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.SourceControl.SourceControlRepository" />
    [Serializable]
    public partial class GitRepository : SourceControlRepository
    {


        /// <summary>
        /// Gets or sets the default branch.
        /// </summary>
        /// <value>The default branch.</value>
        public virtual string DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the branches.
        /// </summary>
        /// <value>The branches.</value>
        public virtual IDictionary<string, Uri> Branches { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepository"/> class.
        /// </summary>
        public GitRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepository"/> class.
        /// </summary>
        public GitRepository(string name) :base(name)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Branches = new Dictionary<string, Uri>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepository"/> class.
        /// </summary>
        public GitRepository(ElementName name) : base(name)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Branches = new Dictionary<string, Uri>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepository"/> class.
        /// </summary>
        public GitRepository(ElementName name, ElementDescription description) : base(name, description)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Branches = new Dictionary<string, Uri>(comparer);
        }
    }
}

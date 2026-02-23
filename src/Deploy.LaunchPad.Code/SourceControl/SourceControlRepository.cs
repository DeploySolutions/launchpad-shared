// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="SourceControlRepository.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Deploy.LaunchPad.Util;
using System.Collections;
using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Code.SourceControl
{
    /// <summary>
    /// Class SourceControlRepository.
    /// Implements the <see cref="Deploy.LaunchPad.Core.SourceControl.ISourceControlRepository" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.SourceControl.ISourceControlRepository" />
    [Serializable]
    public partial class SourceControlRepository : ISourceControlRepository
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual ElementName Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual ElementDescription Description { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public virtual Uri Uri { get; set; }

        public virtual string LocalFilePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlRepository"/> class.
        /// </summary>
        protected SourceControlRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlRepository"/> class.
        /// </summary>
        protected SourceControlRepository(string name)
        {
            Name = new ElementName( name);
            Description = new ElementDescription(name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlRepository"/> class.
        /// </summary>
        protected SourceControlRepository(ElementName name)
        {
            Name = name;
            Description = new ElementDescription(name.Full);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlRepository"/> class.
        /// </summary>
        protected SourceControlRepository(ElementName name, ElementDescription description)
        {
            Name = name;
            Description = description;
        }
    }
}

﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DocumentSetModule.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a set of related documents (Office document (Word, Excel, Powerpoint, RTF) generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class DocumentSetModule :
        LaunchPadGeneratedModule<DocumentSetModuleSettings>
    {
        /// <summary>
        /// Gets or sets the document sets.
        /// </summary>
        /// <value>The document sets.</value>
        public virtual IDictionary<string, DocumentSetComponent> DocumentSets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentSetModule"/> class.
        /// </summary>
        public DocumentSetModule() : base(NullLogger.Instance)
        {
            Settings = new DocumentSetModuleSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            DocumentSets = new Dictionary<string, DocumentSetComponent>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentSetModule"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public DocumentSetModule(ILogger logger) : base(logger)
        {
            Settings = new DocumentSetModuleSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            DocumentSets = new Dictionary<string, DocumentSetComponent>(comparer);
        }

    }
}

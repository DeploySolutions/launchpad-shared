﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a solution generated by LaunchPad Framework which may include code, documents, and static html.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioSolution : LaunchPadGeneratedStaticWebSolution
    {

        /// <summary>
        /// The list of generated Visual Studio modules that belong to this solution.
        /// </summary>
        public virtual IList<LaunchPadGeneratedVisualStudioModule> VisualStudioModules { get; set; }

        /// <summary>
        /// The list of generated document set modules that belong to this solution.
        /// </summary>
        public virtual IList<LaunchPadGeneratedDocumentSetModule> DocumentSetModules { get; set; }

        public LaunchPadGeneratedVisualStudioSolution() : base()
        {
            Version = string.Empty;
            VisualStudioModules = new List<LaunchPadGeneratedVisualStudioModule>();
            DocumentSetModules = new List<LaunchPadGeneratedDocumentSetModule>();
        }
    }
}

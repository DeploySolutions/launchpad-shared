﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a general solution generated by LaunchPad Framework (often used as a convenience for base services to avoid implementing generics.
    /// </summary>
    [Serializable]
    public partial class UnspecifiedSolution : LaunchPadGeneratedSolution
    {
        /// <summary>
        /// The list of generated unspecified/generic modules that belong to this solution.
        /// </summary>
        public virtual IDictionary<string,UnspecifiedModule> Modules { get; set; }

        public UnspecifiedSolution() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Modules = new Dictionary<string, UnspecifiedModule>(comparer);
        }
    }
}

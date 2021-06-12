﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents an unspecified module generated by LaunchPad Framework, containing unspecified components.
    /// </summary>
    [Serializable]
    public partial class UnspecifiedModule : LaunchPadGeneratedModule<UnspecifiedModuleSettings>
    {
        public IDictionary<string,UnspecifiedComponent> Components { get; set; }

        public UnspecifiedModule() : base(NullLogger.Instance)
        {
            Settings = new UnspecifiedModuleSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            Components = new Dictionary<string, UnspecifiedComponent>(comparer);
        }

        public UnspecifiedModule(ILogger logger) : base(logger)
        {
            Settings = new UnspecifiedModuleSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            Components = new Dictionary<string, UnspecifiedComponent>(comparer);
        }

    }
}
﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a set of related documents (Office document (Word, Excel, Powerpoint, RTF) generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public partial class DocumentSetModule :
        LaunchPadGeneratedModule<DocumentSetModuleSettings>
    {
        public virtual IDictionary<string, DocumentSetComponent> DocumentSets { get; set; }

        public DocumentSetModule() : base(NullLogger.Instance)
        {
            Settings = new DocumentSetModuleSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            DocumentSets = new Dictionary<string, DocumentSetComponent>(comparer);
        }

        public DocumentSetModule(ILogger logger) : base(logger)
        {
            Settings = new DocumentSetModuleSettings();
            var comparer = StringComparer.OrdinalIgnoreCase;
            DocumentSets = new Dictionary<string, DocumentSetComponent>(comparer);
        }

    }
}

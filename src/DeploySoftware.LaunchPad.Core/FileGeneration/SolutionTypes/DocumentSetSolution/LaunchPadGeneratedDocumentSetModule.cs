﻿using System;
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
    public partial class LaunchPadGeneratedDocumentSetModule :
        LaunchPadGeneratedModule<LaunchPadGeneratedDocumentSetModuleConfiguration,
            LaunchPadGeneratedDocumentSetComponent,
            LaunchPadGeneratedDocumentSetComponentConfiguration>
    {

        public LaunchPadGeneratedDocumentSetModule() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Components = new Dictionary<string, LaunchPadGeneratedDocumentSetComponent>(comparer); 
        }

    }
}

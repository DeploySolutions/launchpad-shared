﻿using DeploySoftware.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class LaunchPadGeneratedSolutionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, ILaunchPadGeneratedSolutionSettings
    {
        
        public LaunchPadGeneratedSolutionSettings()
        {
        }

    }
}
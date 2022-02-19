using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this object.    
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        [JsonIgnore]
        public ILogger Logger { get; set; }

        protected LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase()
        {
            
            Logger = NullLogger.Instance;
        }

        protected LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase(ILogger logger)
        {
            
            Logger = logger;
        }

        public abstract bool ForCheckingValidity();
        public abstract bool ForInitializing();
        public abstract void ForLoadingFromBlueprintDefinition();
        public abstract void ForAssembling();
        public abstract void ForLoadingTemplates();

        public abstract void ForLoadingTokens();

        public abstract void ForBuilding();

        public abstract void ForPublishing();

        public abstract void ForDisposing();
    }
}

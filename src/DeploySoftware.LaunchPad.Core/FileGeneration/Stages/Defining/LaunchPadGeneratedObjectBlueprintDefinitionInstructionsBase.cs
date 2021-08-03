using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this object.    
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        public IDictionary<string, LaunchPadGeneratedMethod> CustomMethods { get; set; }

        public IDictionary<string, LaunchPadGeneratedProperty> CustomProperties { get; set; }

        public ILogger Logger { get; set; }

        protected LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomMethods = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            Logger = NullLogger.Instance;
        }

        protected LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase(ILogger logger)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomMethods = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
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

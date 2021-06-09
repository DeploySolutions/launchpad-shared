using Castle.Core.Logging;
using System;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this web client component.    
    /// </summary>    
    [Serializable]
    public partial class WebClientBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase
    {
        
        public WebClientBlueprintDefinitionInstructions() : base()
        {
        }

        public WebClientBlueprintDefinitionInstructions(ILogger logger) : base(logger)
        {
        }

        public override void ForAssembling()
        {
            throw new NotImplementedException();
        }

        public override void ForBuilding()
        {
            throw new NotImplementedException();
        }

        public override bool ForCheckingValidity()
        {
            throw new NotImplementedException();
        }

        public override void ForDisposing()
        {
            throw new NotImplementedException();
        }

        public override bool ForInitializing()
        {
            throw new NotImplementedException();
        }

        public override void ForLoadingFromBlueprintDefinition()
        {
            throw new NotImplementedException();
        }

        public override void ForLoadingTemplates()
        {
            throw new NotImplementedException();
        }

        public override void ForLoadingTokens()
        {
            throw new NotImplementedException();
        }

        public override void ForPublishing()
        {
            throw new NotImplementedException();
        }
    }
}

using Castle.Core.Logging;
using DeploySoftware.LaunchPad.FileGeneration.Stages;
using System;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this document set component.    
    /// </summary>    
    [Serializable]
    public partial class DocumentSetBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase
    {
        
        public DocumentSetBlueprintDefinitionInstructions() :base()
        {
        }

        public DocumentSetBlueprintDefinitionInstructions(ILogger logger) : base(logger)
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

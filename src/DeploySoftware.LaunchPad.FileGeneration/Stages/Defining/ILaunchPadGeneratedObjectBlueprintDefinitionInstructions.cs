using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
{
    public interface ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        
        public bool ForCheckingValidity();
        public bool ForInitializing();
        public void ForLoadingFromBlueprintDefinition();
        public void ForAssembling();
        public void ForLoadingTemplates();

        public void ForLoadingTokens();

        public void ForBuilding();

        public void ForPublishing();

        public void ForDisposing();

    }
}

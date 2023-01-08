namespace Deploy.LaunchPad.FileGeneration.Stages
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

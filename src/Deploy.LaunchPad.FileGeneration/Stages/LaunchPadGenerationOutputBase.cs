namespace Deploy.LaunchPad.FileGeneration.Stages
{
    public abstract partial class LaunchPadGenerationOutputBase : LaunchPadGenerationInputBase, ILaunchPadGenerationOutput
    {
        public virtual bool Succeeded { get; set; }
    }
}

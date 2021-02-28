namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {
        /// <summary>
        /// Contains configuration information related to this object's solution (.sln)
        /// </summary>
        public ILaunchPadGeneratedSolutionConfiguration Config { get; set; }

    }
}
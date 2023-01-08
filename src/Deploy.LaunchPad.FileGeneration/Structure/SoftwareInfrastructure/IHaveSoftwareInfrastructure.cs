namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface IHaveSoftwareInfrastructure
    {
        /// <summary>
        /// Describes the overall coding/environment infrastructure in which this element exists 
        /// (ex which version of ABP framework, which cloud provider)
        /// </summary>
        public ISoftwareInfrastructure SoftwareInfrastructure { get; set; }
    }
}

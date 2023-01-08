using Castle.Core.Logging;

namespace Deploy.LaunchPad.Core.Application
{
    /// <summary>
    /// Marker interface for integrating LaunchPad with some external service
    /// </summary>
    public interface ISystemIntegrationService
    {
        public ILogger Logger { get; set; }
    }
}

using Abp.Dependency;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// Marker interface for integrating LaunchPad with some external service
    /// </summary>
    public interface ISystemIntegrationService : ITransientDependency
    {
        public ILogger Logger { get; set; }
    }
}

using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Core.Configuration
{
    /// <summary>
    /// Marker interface for integrating LaunchPad with some external service
    /// </summary>
    public interface ISystemIntegrationService : ISingletonDependency
    {
    }
}

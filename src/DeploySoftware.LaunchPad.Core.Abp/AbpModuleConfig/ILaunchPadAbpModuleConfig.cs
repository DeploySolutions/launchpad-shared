using DeploySoftware.LaunchPad.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleConfig< THostEnvironment> 
        where THostEnvironment : IHostEnvironment
    {

        public THostEnvironment HostEnvironment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }

        
    }
}

﻿using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public abstract partial class LaunchPadAbpModuleConfigBase<THostEnvironment> : ILaunchPadAbpModuleConfig<THostEnvironment>
        where THostEnvironment : IHostEnvironment
    {

        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        public virtual THostEnvironment HostEnvironment { get; set; }

        public virtual IConfigurationRoot ConfigurationRoot { get; set; }


        protected LaunchPadAbpModuleConfigBase(ILogger logger)
        {
            Logger = logger;
        }

    }
}
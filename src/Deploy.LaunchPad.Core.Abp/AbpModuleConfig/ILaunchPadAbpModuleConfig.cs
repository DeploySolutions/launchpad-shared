using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleConfig<THostEnvironment>
        where THostEnvironment : IHostEnvironment
    {

        public THostEnvironment HostEnvironment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }



    }
}

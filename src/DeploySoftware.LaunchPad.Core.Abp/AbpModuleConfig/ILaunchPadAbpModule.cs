
using DeploySoftware.LaunchPad.Core.Config;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModule<TSecretHelper, TAbpModuleHelper>
        where TSecretHelper : ISecretHelper
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper<TSecretHelper>
    {

        public TAbpModuleHelper AbpModuleHelper { get; set; }

    }
}

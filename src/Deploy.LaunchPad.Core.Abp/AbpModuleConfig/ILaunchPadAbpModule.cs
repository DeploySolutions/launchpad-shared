﻿namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModule<TAbpModuleHelper>
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper
    {

        public TAbpModuleHelper AbpModuleHelper { get; set; }

    }
}
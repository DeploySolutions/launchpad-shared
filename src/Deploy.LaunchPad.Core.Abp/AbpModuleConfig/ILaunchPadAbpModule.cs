// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadAbpModule.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Interface ILaunchPadAbpModule
    /// </summary>
    /// <typeparam name="TAbpModuleHelper">The type of the t abp module helper.</typeparam>
    public partial interface ILaunchPadAbpModule<TAbpModuleHelper>
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper
    {

        /// <summary>
        /// Gets or sets the abp module helper.
        /// </summary>
        /// <value>The abp module helper.</value>
        public TAbpModuleHelper AbpModuleHelper { get; set; }

    }
}

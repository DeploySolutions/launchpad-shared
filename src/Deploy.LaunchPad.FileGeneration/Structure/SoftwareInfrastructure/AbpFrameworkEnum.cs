// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AbpFrameworkEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace Deploy.LaunchPad.FileGeneration.Structure
{

    /// <summary>
    /// Enum AbpFrameworkEnum
    /// </summary>
    public enum AbpFrameworkEnum
    {
        /// <summary>
        /// The abp
        /// </summary>
        [Description("Classic free version of AspNetBoilerplate")]
        Abp = 0,
        /// <summary>
        /// The abp ASP net zero
        /// </summary>
        [Description("Commercial version of AspNetBoilerplate")]
        Abp_AspNetZero = 1,
        /// <summary>
        /// The volo abp community
        /// </summary>
        [Description("Volo.Abp aka ABP Community")]
        Volo_ABP_Community = 2,
        /// <summary>
        /// The volo abp commercial
        /// </summary>
        [Description("Volo.Abp aka ABP Commercial")]
        Volo_ABP_Commercial = 3
    }
}

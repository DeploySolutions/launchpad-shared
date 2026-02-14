// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDimension.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Model;
using Deploy.LaunchPad.Util.Data;

namespace Deploy.LaunchPad.Core.Abp.Data
{
    /// <summary>
    /// Describes a dimension for data warehouse reporting. Facts have FK lookups to such dimensions.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    public partial interface IDataDimensionDomainEntity<TPrimaryKey> :
        ILaunchPadDataDimension,
        ILaunchPadDomainEntity<TPrimaryKey>, IMayHaveTenant
    {
    }
}

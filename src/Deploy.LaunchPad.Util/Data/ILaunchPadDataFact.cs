// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IFact.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.Util.Data
{
    /// <summary>
    /// Describes a fact (a "business event-based" data point) for data warehouse reporting purposes.
    /// Facts often FK lookups to related dimensions which help with filtering and qualifying facts.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    public partial interface ILaunchPadDataFact : ILaunchPadDataPoint
    {
    }
}

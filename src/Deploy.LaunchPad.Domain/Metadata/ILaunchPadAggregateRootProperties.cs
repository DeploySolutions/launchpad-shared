// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadAggregateRootProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.Domain.Metadata
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for an Aggregate Root Domain Entity.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface ILaunchPadAggregateRootProperties<TIdType> : ILaunchPadDomainEntityProperties<TIdType>
    {
        /// <summary>
        ///// The fully qualified type names of any children entities (ex. MyCorp.MyApp.Orders.LineItems)
        ///// </summary>
        ///// <value>The children fully qualified types.</value>
        //[DataObjectField(false)]
        //[XmlAttribute]
        //public List<string> ChildrenFullyQualifiedTypes { get; }


    }
}

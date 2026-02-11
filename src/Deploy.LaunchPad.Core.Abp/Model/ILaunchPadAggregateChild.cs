// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadAggregateChild.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Deploy.LaunchPad.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.Model
{
    /// <summary>
    /// Marks an object as a child of a particular Aggregate Root.
    /// Each entity is uniquely identified by its ID, and contains a
    /// set of <see cref="MetadataInformation">MetadataInformation</see>.
    /// Each entity also implements ASP.NET Boilerplate's IAggregateRoot interface.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface ILaunchPadAggregateChild<TIdType> : ILaunchPadDomainEntity<TIdType>,
        ILaunchPadAggregateChildProperties<TIdType>, 
        IComparable<LaunchPadDomainEntityBase<TIdType>>, IEquatable<LaunchPadDomainEntityBase<TIdType>>
    {
    }
}

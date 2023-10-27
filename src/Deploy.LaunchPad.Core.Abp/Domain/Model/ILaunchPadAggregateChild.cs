using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.Domain.Model
{
    /// <summary>
    /// Marks an object as a child of a particular Aggregate Root.
    /// Each entity is uniquely identified by its ID, and contains a 
    /// set of <see cref="MetadataInformation">MetadataInformation</see>.
    /// Each entity also implements ASP.NET Boilerplate's IAggregateRoot interface.
    /// </summary>
    public interface ILaunchPadAggregateChild<TIdType> : ILaunchPadDomainEntity<TIdType>,
        ILaunchPadAggregateChildProperties<TIdType>, 
        IComparable<LaunchPadDomainEntityBase<TIdType>>, IEquatable<LaunchPadDomainEntityBase<TIdType>>
    {
    }
}

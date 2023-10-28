using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    [Serializable]
    public enum DomainEntityType
    {
        DomainEntity = 0,
        AggregateRoot = 1,
        AggregateChild = 2,
        ValueObject = 3
    }
}

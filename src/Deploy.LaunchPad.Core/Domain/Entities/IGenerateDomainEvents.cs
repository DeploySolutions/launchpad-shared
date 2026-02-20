using Deploy.LaunchPad.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Domain.Entities
{
    public partial interface IGenerateDomainEvents
    {
        ICollection<IEventData> DomainEvents { get; }
    }
}

using Deploy.LaunchPad.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Entities
{
    public partial interface IGenerateDomainEvents
    {
        ICollection<IEventData> DomainEvents { get; }
    }
}

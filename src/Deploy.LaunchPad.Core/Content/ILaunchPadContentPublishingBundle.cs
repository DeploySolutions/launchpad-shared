using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Content
{

    public partial interface ILaunchPadContentPublishingBundle<TContentItemId, TSchema>
        where TSchema : Schema.NET.IThing
    {
        public IDictionary<TContentItemId, ILaunchPadContentPublishingItem<TContentItemId, TSchema>> Items { get; }
    }

}

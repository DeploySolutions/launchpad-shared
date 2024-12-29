using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Content
{

    public partial interface ILaunchPadContentPublishingBundle<TContentItemId>
    {
        public IDictionary<TContentItemId, ILaunchPadContentPublishingItem<TContentItemId>> Items { get; }
    }

}

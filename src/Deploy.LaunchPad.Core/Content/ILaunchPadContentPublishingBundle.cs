using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Content
{

    public partial interface ILaunchPadContentPublishingBundle<TContentItemId, TSchema>
            where TSchema : Schema.NET.Thing
    {
        public IList<ILaunchPadContentPublishingItem<TContentItemId, TSchema>> Items { get; }


        public void AddItem(TContentItemId id, ILaunchPadContentPublishingItem<TContentItemId, TSchema> item, bool shouldPreventDuplicates = true);        
    }

}

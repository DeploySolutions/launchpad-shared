using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Content
{

    public partial interface ILaunchPadContentPublishingBundle<TSchema>
            where TSchema : Schema.NET.Thing
    {
        public IList<ILaunchPadContentPublishingItem<TSchema>> Items { get; }


        public void AddItem(Guid id, ILaunchPadContentPublishingItem<TSchema> item, bool shouldPreventDuplicates = true);        
    }

}

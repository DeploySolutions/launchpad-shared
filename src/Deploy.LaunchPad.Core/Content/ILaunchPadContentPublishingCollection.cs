using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Content
{

    public partial interface ILaunchPadContentPublishingCollection<TSchema>
            where TSchema : Schema.NET.Thing
    {
        public IList<ILaunchPadContentPublishingCollectionItem> Items { get; }


        public void AddItem(Guid id, ILaunchPadContentPublishingCollectionItem item, bool shouldPreventDuplicates = true);        
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Content
{

    public partial interface ILaunchPadContentPublishingCollection : IMustHavePublishingInformation
    {
        public IList<ILaunchPadContentPublishingCollectionItem> Items { get; }


        public void AddItem(Guid id, ILaunchPadContentPublishingCollectionItem item, bool shouldPreventDuplicates = true);        
    }

}

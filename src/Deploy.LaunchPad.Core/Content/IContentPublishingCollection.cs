using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Content
{

    public partial interface IContentPublishingCollection : 
        ILaunchPadObject,
        IMustHavePublishingInformation
    {
        public IList<IContentPublishingCollectionItem> Items { get; }


        public void AddItem(Guid id, IContentPublishingCollectionItem item, bool shouldPreventDuplicates = true);        
    }

}

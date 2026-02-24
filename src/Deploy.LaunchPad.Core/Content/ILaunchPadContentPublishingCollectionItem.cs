using Deploy.LaunchPad.Core.Metadata;
using System;
using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Domain.Content
{
    public partial interface ILaunchPadContentPublishingCollectionItem : 
        ILaunchPadObject, 
        ILaunchPadCoreProperties, 
        IMustHavePublishingInformation
    {
        public LaunchPadContentItemType ContentType { get; set; }
        public Uri? ResourceRelativeUri { get; set; }
    }

    public partial interface ILaunchPadContentPublishingItem:
        ILaunchPadContentPublishingCollectionItem
    {        
    }
}

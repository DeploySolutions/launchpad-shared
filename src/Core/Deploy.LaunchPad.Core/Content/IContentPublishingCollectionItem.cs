using Deploy.LaunchPad.Util.Metadata;
using System;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Domain.Content
{
    public partial interface IContentPublishingCollectionItem : 
        ILaunchPadObject, 
        ILaunchPadCoreProperties, 
        IMustHavePublishingInformation, IMustHaveCulture
    {
        public ContentItemType ContentType { get; set; }
        public Uri? ResourceRelativeUri { get; set; }
    }

    public partial interface ILaunchPadContentPublishingItem:
        IContentPublishingCollectionItem
    {        
    }
}

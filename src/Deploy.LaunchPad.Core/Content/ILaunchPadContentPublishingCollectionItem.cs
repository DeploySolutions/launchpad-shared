using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Elements;
using System;

namespace Deploy.LaunchPad.Domain.Content
{
    public partial interface ILaunchPadContentPublishingCollectionItem : ILaunchPadObject, ILaunchPadCoreProperties, IMustHavePublishingInformation
    {
        public Guid Id { get; set; }
        public LaunchPadContentItemType ContentType { get; set; }
        public Uri? ResourceRelativeUri { get; set; }
    }

    public partial interface ILaunchPadContentPublishingItem:
        ILaunchPadContentPublishingCollectionItem
    {        
    }
}

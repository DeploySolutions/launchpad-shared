using Deploy.LaunchPad.Core.Domain.Model;
using System;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface ILaunchPadContentPublishingCollectionItem : ILaunchPadObject, ILaunchPadCommonProperties, IMustHavePublishingInformation
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

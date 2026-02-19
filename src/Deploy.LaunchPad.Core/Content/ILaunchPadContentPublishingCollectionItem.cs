using Deploy.LaunchPad.Core.Metadata;
using System;
using Deploy.LaunchPad.Core.Entities;

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

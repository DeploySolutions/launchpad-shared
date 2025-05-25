using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Core.Schemas.SchemaDotOrg;
using System;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface ILaunchPadContentPublishingCollectionItem : ILaunchPadObject, ILaunchPadCommonProperties
    {
        public Guid Id { get; set; }
        public LaunchPadContentItemType ContentType { get; set; }
        public Uri? ResourceRelativeUri { get; set; }
    }

    public partial interface ILaunchPadContentPublishingItem<TSchema> :
        ILaunchPadContentPublishingCollectionItem,
        IMayHaveSchemaDotOrgProperty<TSchema>
        where TSchema : Schema.NET.Thing
    {        
    }
}

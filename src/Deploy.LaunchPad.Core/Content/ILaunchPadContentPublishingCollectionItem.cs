using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Core.Schemas.SchemaDotOrg;
using System;

namespace Deploy.LaunchPad.Core.Content
{
    public interface ILaunchPadContentPublishingCollectionItem : ILaunchPadObject, ILaunchPadCommonProperties
    {
        Guid Id { get; set; }
        LaunchPadContentItemType ContentType { get; set; }
    }

    public partial interface ILaunchPadContentPublishingItem<TSchema> :
        ILaunchPadContentPublishingCollectionItem,
        IMayHaveSchemaDotOrgProperty<TSchema>
        where TSchema : Schema.NET.Thing
    {        
    }
}

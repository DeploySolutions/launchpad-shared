using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Core.Schemas.SchemaDotOrg;
using System;

namespace Deploy.LaunchPad.Core.Content
{
    public interface ILaunchPadContentPublishingItem : ILaunchPadObject, ILaunchPadCommonProperties
    {
        Guid Id { get; set; }
        LaunchPadContentItemType ContentType { get; set; }
    }

    public partial interface ILaunchPadContentPublishingItem<TSchema> :
        ILaunchPadContentPublishingItem,
        IMayHaveSchemaDotOrgProperty<TSchema>
        where TSchema : Schema.NET.Thing
    {        
    }
}

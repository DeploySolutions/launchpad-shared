using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Core.Schemas.SchemaDotOrg;
using System;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface ILaunchPadContentPublishingItem<TSchema> : 
        ILaunchPadCommonProperties,
        IMayHaveSchemaDotOrgProperty<TSchema>
        where TSchema : Schema.NET.Thing
    {
        Guid Id { get; set; }

        LaunchPadContentItemType ContentType { get; set; }
    }
}

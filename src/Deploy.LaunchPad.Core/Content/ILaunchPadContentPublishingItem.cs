using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Core.Schemas.SchemaDotOrg;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface ILaunchPadContentPublishingItem<TIdType, TSchema> : 
        ILaunchPadCommonProperties,
        IMayHaveSchemaDotOrgProperty<TSchema>
        where TSchema : Schema.NET.Thing
    {
        TIdType Id { get; set; }

        LaunchPadContentItemType ContentType { get; set; }
    }
}

using Deploy.LaunchPad.Core.Domain.Model;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface ILaunchPadContentPublishingItem<TIdType> : ILaunchPadCommonProperties
    {
        TIdType Id { get; set; }

        LaunchPadContentItemType ContentType { get; set; }
    }
}

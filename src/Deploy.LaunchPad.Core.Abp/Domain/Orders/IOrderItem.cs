
namespace Deploy.LaunchPad.Core.Abp.Domain.Orders
{
    public partial interface IOrderItem<TItemId> : ILaunchPadDomainEntity<TItemId>, ICanBeOrdered
    {
    }
}

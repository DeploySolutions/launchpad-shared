using System;

namespace Deploy.LaunchPad.Core.Abp.Domain.Orders
{
    [Serializable]
    public  abstract partial class OrderItemBase<TIdType> : LaunchPadDomainEntityBase<TIdType>, IOrderItem<TIdType>
    {

        protected OrderItemBase() { }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain.Orders
{
    public partial interface IOrder<TOrderId, TUserId, TItemId> : ILaunchPadAggregateRoot<TOrderId>
    {
        ///<summary>
        /// FK id for the User who placed the order
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TUserId UserId { get; set; }

        ///<summary>
        /// The order status
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public System.String Status { get; set; }

        /// <summary>
        /// The list of Order Items contained within
        /// </summary>
        public IDictionary<TItemId, IOrderItem<TItemId>> Items { get; set; }

    }
}

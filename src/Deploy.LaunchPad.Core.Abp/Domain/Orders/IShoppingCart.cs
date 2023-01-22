using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain.Orders
{
    public partial interface IShoppingCart<TShoppingCartId, TUserId, TItemId> : ILaunchPadDomainEntity<TShoppingCartId>
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
        /// The list of order-able items contained within the cart
        /// </summary>
        public IDictionary<TItemId, ICanBeOrdered> Items { get; set; }

    }
}

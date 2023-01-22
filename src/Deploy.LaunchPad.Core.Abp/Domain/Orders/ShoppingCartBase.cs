using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain.Orders
{
    [Serializable]
    public abstract partial class ShoppingCartBase<TId, TItemId, TUserId> : LaunchPadDomainEntityBase<TId>
    {

        /// <summary>
        /// The list of order-able items contained within the cart
        /// </summary>
        public IDictionary<TItemId, ICanBeOrdered> Items { get; set; }

        ///<summary>
        /// FK id for the User who placed the order
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual TUserId UserId { get; set; }

        ///<summary>
        /// The order status
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual System.String Status { get; set; }

        protected ShoppingCartBase()
        {
            Items = new Dictionary<TItemId, ICanBeOrdered>();
        }
    }
}

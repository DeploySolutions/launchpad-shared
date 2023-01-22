using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain.Orders
{
    [Serializable]
    public abstract partial class OrderBase<TOrderId, TItemId, TUserId> : LaunchPadAggregateRootBase<TOrderId>
    {
        /// <summary>
        /// The list of Order Items contained within
        /// </summary>
        public virtual IDictionary<TItemId, IOrderItem<TItemId>> Items { get; set; }

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

        protected OrderBase()
        {
            Items = new Dictionary<TItemId, IOrderItem<TItemId>>();
        }
    }
}

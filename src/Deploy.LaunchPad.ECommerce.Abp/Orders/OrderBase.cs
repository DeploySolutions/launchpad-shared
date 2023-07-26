using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
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
        public virtual string Status { get; set; }


        ///<summary>
        /// Whether the order is empty
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsEmpty { get { return Items.Count <= 0; } }

        ///<summary>
        /// The total number of items in the order
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool TotalItems { get { return Items.Count <= 0; } }

        ///<summary>
        /// The maximum number of items the cart can have
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual int MaxItems { get; }

        public abstract void AddItem(ICanBeOrdered item);
        
        public abstract void RemoveItem(ICanBeOrdered item);

        protected OrderBase()
        {
            Items = new Dictionary<TItemId, IOrderItem<TItemId>>();
            MaxItems = 10;
        }
    }
}

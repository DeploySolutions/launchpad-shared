using Deploy.LaunchPad.Core.Abp.Domain;
using Deploy.LaunchPad.ECommerce.Abp.Carts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    [Serializable]
    public abstract partial class CartBase<TCartId, TItemId, TUserId> : LaunchPadAggregateRootBase<TCartId>, ICart<TCartId, TItemId, TUserId>
    {

        /// <summary>
        /// The list of order-able items contained within the cart
        /// </summary>
        public virtual IDictionary<TItemId, ICanBeOrdered> Items { get; protected set; }

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


        ///<summary>
        /// Whether the cart is empty
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsEmpty { get { return Items.Count <= 0; } }

        ///<summary>
        /// The total number of items currently in the cart
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

        protected CartBase()
        {
            Items = new Dictionary<TItemId, ICanBeOrdered>();
            MaxItems = 10;
        }

        public abstract IDictionary<TItemId, ICanBeOrdered> GetCartItems();

        public abstract void AddItem(ICanBeOrdered item);

        public abstract void RemoveItem(ICanBeOrdered item);
    }
}

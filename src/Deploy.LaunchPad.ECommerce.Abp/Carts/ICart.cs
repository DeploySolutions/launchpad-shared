using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.ECommerce.Abp.Orders;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Carts
{
    public partial interface ICart<TCartId, TItemId, TUserId> : ILaunchPadAggregateRoot<TCartId>
    {
        ///<summary>
        /// FK id for the User who placed the order
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TUserId UserId { get; set; }

        ///<summary>
        /// The cart status
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Status { get; set; }


        ///<summary>
        /// Is the carty empty
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsEmpty { get; }

        ///<summary>
        /// The maximum number of items the cart can have
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public int MaxItems { get; }

        ///<summary>
        /// The total number of items currently in the cart
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool TotalItems { get; }

        /// <summary>
        /// The list of order-able items contained within the cart
        /// </summary>
        public IDictionary<TItemId, ICanBeOrdered> Items { get; }

        public IDictionary<TItemId, ICanBeOrdered> GetCartItems();


        public void AddItem(ICanBeOrdered item);
        public void RemoveItem(ICanBeOrdered item);

    }
}

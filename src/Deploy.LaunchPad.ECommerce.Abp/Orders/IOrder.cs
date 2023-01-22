using Deploy.LaunchPad.Core.Abp.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    public partial interface IOrder<TOrderId, TItemId, TUserId > : ILaunchPadAggregateRoot<TOrderId>
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
        public string Status { get; set; }

        /// <summary>
        /// The list of Order Items contained within
        /// </summary>
        public IDictionary<TItemId, IOrderItem<TItemId>> Items { get; }

        ///<summary>
        /// Is the order empty
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsEmpty { get; }


        ///<summary>
        /// The total number of items in the order
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool TotalItems { get; }

        ///<summary>
        /// The maximum number of items the cart can have
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public int MaxItems { get; }

        public void AddItem(ICanBeOrdered item);
        public void RemoveItem(ICanBeOrdered item);

    }
}

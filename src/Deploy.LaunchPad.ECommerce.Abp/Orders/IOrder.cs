// ***********************************************************************
// Assembly         : Deploy.LaunchPad.ECommerce.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IOrder.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    /// <summary>
    /// Interface IOrder
    /// Extends the <see cref="ILaunchPadAggregateRoot{TOrderId}" />
    /// </summary>
    /// <typeparam name="TOrderId">The type of the t order identifier.</typeparam>
    /// <typeparam name="TItemId">The type of the t item identifier.</typeparam>
    /// <typeparam name="TUserId">The type of the t user identifier.</typeparam>
    /// <seealso cref="ILaunchPadAggregateRoot{TOrderId}" />
    public partial interface IOrder<TOrderId, TItemId, TUserId > : ILaunchPadAggregateRoot<TOrderId>
    {
        /// <summary>
        /// FK id for the User who placed the order
        /// </summary>
        /// <value>The user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public TUserId UserId { get; set; }

        /// <summary>
        /// The order status
        /// </summary>
        /// <value>The status.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// The list of Order Items contained within
        /// </summary>
        /// <value>The items.</value>
        public IDictionary<TItemId, IOrderItem<TItemId>> Items { get; }

        /// <summary>
        /// Is the order empty
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsEmpty { get; }


        /// <summary>
        /// The total number of items in the order
        /// </summary>
        /// <value><c>true</c> if [total items]; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool TotalItems { get; }

        /// <summary>
        /// The maximum number of items the cart can have
        /// </summary>
        /// <value>The maximum items.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public int MaxItems { get; }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(ICanBeOrdered item);
        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveItem(ICanBeOrdered item);

    }
}

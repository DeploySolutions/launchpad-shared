// ***********************************************************************
// Assembly         : Deploy.LaunchPad.ECommerce.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ICart.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.ECommerce.Abp.Orders;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Carts
{
    /// <summary>
    /// Interface ICart
    /// Extends the <see cref="ILaunchPadAggregateRoot{TCartId}" />
    /// </summary>
    /// <typeparam name="TCartId">The type of the t cart identifier.</typeparam>
    /// <typeparam name="TItemId">The type of the t item identifier.</typeparam>
    /// <typeparam name="TUserId">The type of the t user identifier.</typeparam>
    /// <seealso cref="ILaunchPadAggregateRoot{TCartId}" />
    public partial interface ICart<TCartId, TItemId, TUserId> : ILaunchPadAggregateRoot<TCartId>
    {
        /// <summary>
        /// FK id for the User who placed the order
        /// </summary>
        /// <value>The user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public TUserId UserId { get; set; }

        /// <summary>
        /// The cart status
        /// </summary>
        /// <value>The status.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Status { get; set; }


        /// <summary>
        /// Is the carty empty
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsEmpty { get; }

        /// <summary>
        /// The maximum number of items the cart can have
        /// </summary>
        /// <value>The maximum items.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public int MaxItems { get; }

        /// <summary>
        /// The total number of items currently in the cart
        /// </summary>
        /// <value><c>true</c> if [total items]; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool TotalItems { get; }

        /// <summary>
        /// The list of order-able items contained within the cart
        /// </summary>
        /// <value>The items.</value>
        public IDictionary<TItemId, ICanBeOrdered> Items { get; }

        /// <summary>
        /// Gets the cart items.
        /// </summary>
        /// <returns>IDictionary&lt;TItemId, ICanBeOrdered&gt;.</returns>
        public IDictionary<TItemId, ICanBeOrdered> GetCartItems();


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

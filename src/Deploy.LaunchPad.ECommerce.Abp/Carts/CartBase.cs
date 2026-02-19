// ***********************************************************************
// Assembly         : Deploy.LaunchPad.ECommerce.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="CartBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.ECommerce.Abp.Carts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    /// <summary>
    /// Class CartBase.
    /// Implements the <see cref="LaunchPadAggregateRootBase{TCartId}" />
    /// Implements the <see cref="ICart{TCartId, TItemId, TUserId}" />
    /// </summary>
    /// <typeparam name="TCartId">The type of the t cart identifier.</typeparam>
    /// <typeparam name="TItemId">The type of the t item identifier.</typeparam>
    /// <typeparam name="TUserId">The type of the t user identifier.</typeparam>
    /// <seealso cref="LaunchPadAggregateRootBase{TCartId}" />
    /// <seealso cref="ICart{TCartId, TItemId, TUserId}" />
    [Serializable]
    public abstract partial class CartBase<TCartId, TItemId, TUserId> : LaunchPadAggregateRootBase<TCartId>, ICart<TCartId, TItemId, TUserId>
    {

        /// <summary>
        /// The list of order-able items contained within the cart
        /// </summary>
        /// <value>The items.</value>
        public virtual IDictionary<TItemId, ICanBeOrdered> Items { get; protected set; }

        /// <summary>
        /// FK id for the User who placed the order
        /// </summary>
        /// <value>The user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual TUserId UserId { get; set; }

        /// <summary>
        /// The order status
        /// </summary>
        /// <value>The status.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual System.String Status { get; set; }


        /// <summary>
        /// Whether the cart is empty
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsEmpty { get { return Items.Count <= 0; } }

        /// <summary>
        /// The total number of items currently in the cart
        /// </summary>
        /// <value><c>true</c> if [total items]; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool TotalItems { get { return Items.Count <= 0; } }


        /// <summary>
        /// The maximum number of items the cart can have
        /// </summary>
        /// <value>The maximum items.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual int MaxItems { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartBase{TCartId, TItemId, TUserId}"/> class.
        /// </summary>
        protected CartBase()
        {
            Items = new Dictionary<TItemId, ICanBeOrdered>();
            MaxItems = 10;
        }

        /// <summary>
        /// Gets the cart items.
        /// </summary>
        /// <returns>IDictionary&lt;TItemId, ICanBeOrdered&gt;.</returns>
        public abstract IDictionary<TItemId, ICanBeOrdered> GetCartItems();

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public abstract void AddItem(ICanBeOrdered item);

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public abstract void RemoveItem(ICanBeOrdered item);
    }
}

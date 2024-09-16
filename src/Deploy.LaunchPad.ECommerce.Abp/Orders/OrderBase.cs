// ***********************************************************************
// Assembly         : Deploy.LaunchPad.ECommerce.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="OrderBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    /// <summary>
    /// Class OrderBase.
    /// Implements the <see cref="LaunchPadAggregateRootBase{TOrderId}" />
    /// </summary>
    /// <typeparam name="TOrderId">The type of the t order identifier.</typeparam>
    /// <typeparam name="TItemId">The type of the t item identifier.</typeparam>
    /// <typeparam name="TUserId">The type of the t user identifier.</typeparam>
    /// <seealso cref="LaunchPadAggregateRootBase{TOrderId}" />
    [Serializable]
    public abstract partial class OrderBase<TOrderId, TItemId, TUserId> : LaunchPadAggregateRootBase<TOrderId>
    {
        /// <summary>
        /// The list of Order Items contained within
        /// </summary>
        /// <value>The items.</value>
        public virtual IDictionary<TItemId, IOrderItem<TItemId>> Items { get; set; }

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
        public virtual string Status { get; set; }


        /// <summary>
        /// Whether the order is empty
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsEmpty { get { return Items.Count <= 0; } }

        /// <summary>
        /// The total number of items in the order
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
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public abstract void AddItem(ICanBeOrdered item);

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public abstract void RemoveItem(ICanBeOrdered item);

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBase{TOrderId, TItemId, TUserId}"/> class.
        /// </summary>
        protected OrderBase()
        {
            Items = new Dictionary<TItemId, IOrderItem<TItemId>>();
            MaxItems = 10;
        }
    }
}

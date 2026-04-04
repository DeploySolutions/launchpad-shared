// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Domain.ECommerce.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IOrderItem.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain.Entities;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.ECommerce.Orders
{
    /// <summary>
    /// Interface IOrderItem
    /// Extends the <see cref="IDomainEntity{TItemId}" />
    /// Extends the <see cref="Deploy.LaunchPad.Domain.ECommerce.Orders.ICanBeOrdered" />
    /// </summary>
    /// <typeparam name="TItemId">The type of the t item identifier.</typeparam>
    /// <seealso cref="IDomainEntity{TItemId}" />
    /// <seealso cref="Deploy.LaunchPad.Domain.ECommerce.Orders.ICanBeOrdered" />
    public partial interface IOrderItem<TItemId> : IDomainEntity<TItemId>, ICanBeOrdered
    {

        /// <summary>
        /// The price of the item
        /// </summary>
        /// <value>The price.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public double Price { get; set; }
    }
}

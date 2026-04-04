// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Domain.ECommerce.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="OrderItemBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Domain.Entities;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.ECommerce.Orders
{
    /// <summary>
    /// Class OrderItemBase.
    /// Implements the <see cref="DomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="Deploy.LaunchPad.Domain.ECommerce.Abp.Orders.IOrderItem{TPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    /// <seealso cref="DomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="Deploy.LaunchPad.Domain.ECommerce.Abp.Orders.IOrderItem{TPrimaryKey}" />
    [Serializable]
    public  abstract partial class OrderItemBase<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IOrderItem<TPrimaryKey>
    {

        /// <summary>
        /// The price of the
        /// </summary>
        /// <value>The price.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Price { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemBase{TPrimaryKey}"/> class.
        /// </summary>
        protected OrderItemBase() { }
    }
}

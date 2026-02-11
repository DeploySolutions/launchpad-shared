// ***********************************************************************
// Assembly         : Deploy.LaunchPad.ECommerce.Abp
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
using Deploy.LaunchPad.Core.Abp.Model;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    /// <summary>
    /// Class OrderItemBase.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TIdType}" />
    /// Implements the <see cref="Deploy.LaunchPad.ECommerce.Abp.Orders.IOrderItem{TIdType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TIdType}" />
    /// <seealso cref="Deploy.LaunchPad.ECommerce.Abp.Orders.IOrderItem{TIdType}" />
    [Serializable]
    public  abstract partial class OrderItemBase<TIdType> : LaunchPadDomainEntityBase<TIdType>, IOrderItem<TIdType>
    {

        /// <summary>
        /// The price of the
        /// </summary>
        /// <value>The price.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Price { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemBase{TIdType}"/> class.
        /// </summary>
        protected OrderItemBase() { }
    }
}

using Deploy.LaunchPad.Core.Abp.Domain;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    [Serializable]
    public  abstract partial class OrderItemBase<TIdType> : LaunchPadDomainEntityBase<TIdType>, IOrderItem<TIdType>
    {

        ///<summary>
        /// The price of the 
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Price { get; set; }

        protected OrderItemBase() { }
    }
}

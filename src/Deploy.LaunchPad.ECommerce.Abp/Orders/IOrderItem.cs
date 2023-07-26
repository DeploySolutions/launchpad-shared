using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.ECommerce.Abp.Orders
{
    public partial interface IOrderItem<TItemId> : ILaunchPadDomainEntity<TItemId>, ICanBeOrdered
    {

        ///<summary>
        /// The price of the item
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public double Price { get; set; }
    }
}

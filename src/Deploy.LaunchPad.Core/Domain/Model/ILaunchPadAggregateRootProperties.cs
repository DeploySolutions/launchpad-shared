using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for an Aggregate Root Domain Entity. 
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public interface ILaunchPadAggregateRootProperties<TIdType> : ILaunchPadDomainEntityProperties<TIdType>
    {
        /// <summary>
        /// The fully qualified type names of any children entities (ex. MyCorp.MyApp.Orders.LineItems)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public HashSet<string> ChildrenFullyQualifiedTypes { get; }


    }
}

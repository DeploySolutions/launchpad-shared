using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a child entity of an Aggregate Root. 
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public interface ILaunchPadAggregateChildProperties<TIdType> : ILaunchPadDomainEntityProperties<TIdType>
    {
        /// <summary>
        /// The fully qualified type name of the parent Aggregate Root
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string ParentFullyQualifiedType { get; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMustHaveUnitOfMeasure
    {
        ///<summary>
        /// Unit of measure (ex "kg", "m", "cm", "mm", "g", "l", "ml", "oz", "in", "ft", etc.)
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string UnitOfMeasure { get; set; }

    }
}

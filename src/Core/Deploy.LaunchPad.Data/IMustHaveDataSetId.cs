using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Data
{
    public partial interface IMustHaveDataSetId<TPrimaryKey>
    {
        ///<summary>
        /// DataSet Id
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TPrimaryKey DataSetId { get; set; }

    }
}

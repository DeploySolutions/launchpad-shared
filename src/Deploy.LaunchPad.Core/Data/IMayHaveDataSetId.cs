using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Data
{
    public partial interface IMayHaveDataSetId<TIdType>
    {
        ///<summary>
        /// DataSet Id
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TIdType? DataSetId { get; set; }

    }
}

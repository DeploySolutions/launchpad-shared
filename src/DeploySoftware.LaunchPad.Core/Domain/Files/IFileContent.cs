using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public partial interface IFileContent<TFileContentType>
        where TFileContentType: class
    {
        /// <summary>
        /// Get the content/data of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public TFileContentType Content { get; set; }
    }
}

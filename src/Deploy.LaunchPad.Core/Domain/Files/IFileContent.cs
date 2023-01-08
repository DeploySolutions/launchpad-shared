using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain
{
    public partial interface IFileContent<TFileContentType>
        where TFileContentType : class
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

using Castle.Core.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain
{
    public interface IFileProvider
    {
        [NotMapped]
        public ILogger Logger { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string Id { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string Name { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string Description { get; set; }

        /// <summary>
        /// Contains a dictionary of file locations, keyed using the friendly name of the location. 
        /// Each location contains the instruct
        /// </summary>        
        [DataObjectField(false)]
        [XmlAttribute]
        public Dictionary<string, IFileStorageLocation> Locations { get; set; }

        // get methods
        public IFileStorageLocation GetLocationById(string id, string caller);

        public Task<IFileStorageLocation> GetLocationByIdAsync(string id, string caller);


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IMayHaveLastModificationTimestamp
    {

        /// <summary>
        /// Gets or sets the last modification time.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public DateTime? LastModificationTime { get; }

    }
}

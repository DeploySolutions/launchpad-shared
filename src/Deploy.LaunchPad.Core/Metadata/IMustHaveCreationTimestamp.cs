using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMustHaveCreationTimestamp
    {

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public DateTime CreationTime { get; }

    }
}

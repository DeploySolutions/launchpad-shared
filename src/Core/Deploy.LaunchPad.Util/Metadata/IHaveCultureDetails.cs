using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IHaveCultureDetails
    {
        string DefaultCultureName { get; }

        /// <summary>
        /// The default culture of the containing object
        /// </summary>
        /// <value>The culture default.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        CultureInfo DefaultCulture
        {
            get; 
        }

        /// <summary>
        /// The comma-delimited list of cultures supported by the containing object
        /// </summary>
        /// <value>The culture supported.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        IList<CultureInfo> SupportedCultures
        {
            get; 
        }

    }
}

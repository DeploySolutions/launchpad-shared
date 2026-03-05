using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial class CultureDetails : IHaveCultureDetails
    {
        public string DefaultCultureName { get { return DefaultCulture.Name; } }

        /// <summary>
        /// The default culture of the containing object
        /// </summary>
        /// <value>The culture default.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public required CultureInfo DefaultCulture
        {
            get; init;
        } = new CultureInfo("en");

        /// <summary>
        /// The comma-delimited list of cultures supported by the containing object
        /// </summary>
        /// <value>The culture supported.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public IList<CultureInfo> SupportedCultures
        {
            get; 
        }

        [SetsRequiredMembers]
        public CultureDetails()
        {
            SupportedCultures = new List<CultureInfo>();
            SupportedCultures.Add(DefaultCulture);
        }


        [SetsRequiredMembers]
        public CultureDetails(CultureInfo defaultCulture)
        {
            DefaultCulture = defaultCulture;
            SupportedCultures = new List<CultureInfo>();
            SupportedCultures.Add(DefaultCulture);
        }

        [SetsRequiredMembers]
        public CultureDetails(CultureInfo defaultCulture, IList<CultureInfo> supportedCultures)
        {
            DefaultCulture = defaultCulture;
            SupportedCultures = supportedCultures;
            if(!SupportedCultures.Contains(defaultCulture))
            {
                SupportedCultures.Add(DefaultCulture);
            }
        }

        [SetsRequiredMembers]
        public CultureDetails(IList<CultureInfo> supportedCultures)
        {
            SupportedCultures = supportedCultures;
            DefaultCulture = supportedCultures.FirstOrDefault();
        }
    }
}

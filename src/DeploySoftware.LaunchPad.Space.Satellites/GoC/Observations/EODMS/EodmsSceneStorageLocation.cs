using DeploySoftware.LaunchPad.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Space.Satellites.GoC.EODMS
{
    [Owned]
    public partial class EodmsSceneStorageLocation : FileStorageLocationBase
    {
        public const string EODMS_URI = "https://www.eodms-sgdot.nrcan-rncan.gc.ca/";

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual EodmsObservationCollections Collection { get; set; } = EodmsObservationCollections.RCMImageProducts;

        /// <summary>
        /// Creates a EODMS location object.
        /// </summary>
        public EodmsSceneStorageLocation()
        {
            RootPath = new Uri(EODMS_URI);
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected EodmsSceneStorageLocation(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Collection = (EodmsObservationCollections)info.GetValue("Collection", typeof(EodmsObservationCollections));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Collection", Collection);
        }

    }
}


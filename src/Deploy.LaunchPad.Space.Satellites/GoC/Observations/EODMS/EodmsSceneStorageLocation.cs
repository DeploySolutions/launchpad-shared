// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="EodmsSceneStorageLocation.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Space.Satellites.GoC.EODMS
{
    /// <summary>
    /// Class EodmsSceneStorageLocation.
    /// Implements the <see cref="GenericFileStorageLocation" />
    /// </summary>
    /// <seealso cref="GenericFileStorageLocation" />
    [Owned]
    public partial class EodmsSceneStorageLocation : GenericFileStorageLocation
    {
        /// <summary>
        /// The eodms URI
        /// </summary>
        public const string EODMS_URI = "https://www.eodms-sgdot.nrcan-rncan.gc.ca/";

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        /// <value>The collection.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual EodmsObservationCollections Collection { get; set; } = EodmsObservationCollections.RCMImageProducts;

        /// <summary>
        /// Creates a EODMS location object.
        /// </summary>
        public EodmsSceneStorageLocation()
        {
            RootUri = new Uri(EODMS_URI);
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

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Collection", Collection);
        }

    }
}


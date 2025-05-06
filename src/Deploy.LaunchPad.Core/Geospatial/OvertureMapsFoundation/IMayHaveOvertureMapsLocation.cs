// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMayHaveOvertureMapsDefinition.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Geospatial.Overture
{
    /// <summary>
    /// Interface IMayHaveOvertureMapsDefinition
    /// </summary>
    public partial interface IMayHaveOvertureMapsLocation
    {
        /// <summary>
        /// Gets or sets the GERS ID.
        /// </summary>
        /// <value>The index of the h3.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public Guid? GERSId { get; set; }

        /// <summary>
        /// Describes any Overture Maps json features for the asset, if any. Facilitates data overlays.
        /// </summary>
        /// <value>The index of the h3.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string? Features { get; set; }
    }
}
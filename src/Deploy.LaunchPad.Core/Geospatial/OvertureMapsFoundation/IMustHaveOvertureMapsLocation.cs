﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMustHaveOvertureMapsLocation.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain.Model;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Geospatial.Overture
{
    /// <summary>
    /// Interface IMustHaveOvertureMapsLocation
    /// </summary>
    public partial interface IMustHaveOvertureMapsLocation : ILaunchPadObject
    {
        ///<summary>
        /// Overture Map's location details includnig Global Entity Reference System (GERS) ID, if known.
        /// <seealso cref="https://docs.overturemaps.org/gers/">Global Entity Reference System (GERS)</seealso>
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public OvertureMapsLocation OvertureMapsLocation { get; set; }

    }
}
// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMustHaveGeoJsonDefinition.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy.LaunchPad.Geospatial.GeoJson
{
    /// <summary>
    /// Interface IMustHaveGeoJsonDefinition
    /// </summary>
    public partial interface IMustHaveGeoJsonDefinition
    {

        /// <summary>
        /// Gets the geo json.
        /// </summary>
        /// <value>The geo json.</value>
        public string GeoJson { get; }

    }
}
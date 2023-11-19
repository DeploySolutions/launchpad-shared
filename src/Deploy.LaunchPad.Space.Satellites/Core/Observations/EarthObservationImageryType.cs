// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="EarthObservationImageryType.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.Space.Satellites.Core
{
    /// <summary>
    /// Aligns to the NASA Earth Observing System Data and Information System (EOSDIS) data product levels.
    /// https://earthdata.nasa.gov/collaborate/open-data-services-and-software/data-information-policy/data-levels
    /// </summary>
    public enum EarthObservationImageryType
    {
        /// <summary>
        /// The panchromatic
        /// </summary>
        Panchromatic = 0,
        /// <summary>
        /// The multispectral
        /// </summary>
        Multispectral = 1,
        /// <summary>
        /// The pan sharpened
        /// </summary>
        PanSharpened = 2,
        /// <summary>
        /// The hyperspectral
        /// </summary>
        Hyperspectral = 3,
        /// <summary>
        /// The microwave radiometry
        /// </summary>
        MicrowaveRadiometry = 4,
        /// <summary>
        /// The synthetic aperture radar
        /// </summary>
        SyntheticApertureRadar = 5,
        /// <summary>
        /// The lidar
        /// </summary>
        Lidar = 6,
        /// <summary>
        /// The radar altimetry
        /// </summary>
        RadarAltimetry = 7,
        /// <summary>
        /// The GNSSR
        /// </summary>
        GNSSR = 8,
        /// <summary>
        /// The radar scatterometry
        /// </summary>
        RadarScatterometry = 9,
        /// <summary>
        /// The aerial
        /// </summary>
        Aerial = 10
    }
}

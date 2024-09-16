// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="EOSDISLevelEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.Space.Satellites.Core
{
    /// <summary>
    /// Aligns to the NASA Earth Observing System Data and Information System (EOSDIS) data product levels.
    /// https://earthdata.nasa.gov/collaborate/open-data-services-and-software/data-information-policy/data-levels
    /// </summary>
    public enum EOSDISLevelEnum
    {
        /// <summary>
        /// The level0
        /// </summary>
        Level0 = 0,
        /// <summary>
        /// The level1 a
        /// </summary>
        Level1A = 1,
        /// <summary>
        /// The level1 b
        /// </summary>
        Level1B = 2,
        /// <summary>
        /// The level1 c
        /// </summary>
        Level1C = 3,
        /// <summary>
        /// The level2
        /// </summary>
        Level2 = 4,
        /// <summary>
        /// The level2 a
        /// </summary>
        Level2A = 5,
        /// <summary>
        /// The level2 b
        /// </summary>
        Level2B = 6,
        /// <summary>
        /// The level3
        /// </summary>
        Level3 = 7,
        /// <summary>
        /// The level3 a
        /// </summary>
        Level3A = 8,
        /// <summary>
        /// The level4
        /// </summary>
        Level4 = 9

    }
}

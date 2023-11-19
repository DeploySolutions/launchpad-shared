// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="EodmsObservationCollections.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace Deploy.LaunchPad.Space.Satellites.GoC.EODMS
{
    /// <summary>
    /// Lists the aerial and satellite imagery collections (publicly) available in
    /// NRCan's Earth Observation Data Management System (EODMS). Note some collection names
    /// have hyphens in EODMS which cannot be used as enum names but are included in the description attribute.
    /// </summary>
    [Serializable()]
    public enum EodmsObservationCollections
    {
        /// <summary>
        /// The napl
        /// </summary>
        [Description("NAPL")]
        NAPL,
        /// <summary>
        /// The SGB air photos
        /// </summary>
        [Description("SGBAirPhotos")]
        SGBAirPhotos,
        /// <summary>
        /// The RCM image products
        /// </summary>
        [Description("RCMImageProducts")]
        RCMImageProducts,
        /// <summary>
        /// The cosmo sky med1
        /// </summary>
        [Description("COSMO-SkyMed1")]
        COSMOSkyMed1,
        /// <summary>
        /// The radarsat1
        /// </summary>
        [Description("Radarsat1")]
        Radarsat1,
        /// <summary>
        /// The radarsat1 raw products
        /// </summary>
        [Description("Radarsat1RawProducts")]
        Radarsat1RawProducts,
        /// <summary>
        /// The radarsat2
        /// </summary>
        [Description("Radarsat2")]
        Radarsat2,
        /// <summary>
        /// The radarsat2 raw products
        /// </summary>
        [Description("Radarsat2RawProducts")]
        Radarsat2RawProducts,
        /// <summary>
        /// The RCM science data
        /// </summary>
        [Description("RCMScienceData")]
        RCMScienceData,
        /// <summary>
        /// The terra sar x
        /// </summary>
        [Description("TerraSarX")]
        TerraSarX,
        /// <summary>
        /// The DMC
        /// </summary>
        [Description("DMC")]
        DMC,
        /// <summary>
        /// The gaofen1
        /// </summary>
        [Description("Gaofen-1")]
        Gaofen1,
        /// <summary>
        /// The geo eye1
        /// </summary>
        [Description("GeoEye-1")]
        GeoEye1,
        /// <summary>
        /// The ikonos
        /// </summary>
        [Description("IKONOS")]
        IKONOS,
        /// <summary>
        /// The irs
        /// </summary>
        [Description("IRS")]
        IRS,
        /// <summary>
        /// The planet scope
        /// </summary>
        [Description("PlanetScope")]
        PlanetScope,
        /// <summary>
        /// The quick bird2
        /// </summary>
        [Description("QuickBird-2")]
        QuickBird2,
        /// <summary>
        /// The rapid eye
        /// </summary>
        [Description("RapidEye")]
        RapidEye,
        /// <summary>
        /// The spot
        /// </summary>
        [Description("SPOT")]
        SPOT,
        /// <summary>
        /// The world view1
        /// </summary>
        [Description("WorldView-1")]
        WorldView1,
        /// <summary>
        /// The world view2
        /// </summary>
        [Description("WorldView-2")]
        WorldView2,
        /// <summary>
        /// The world view3
        /// </summary>
        [Description("WorldView-3")]
        WorldView3,
        /// <summary>
        /// The vasp
        /// </summary>
        [Description("VASP")]
        VASP
    }
}

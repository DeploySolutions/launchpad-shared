using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.GoC
{
    /// <summary>
    /// Lists the aerial and satellite imagery collections (publicly) available in 
    /// NRCan's Earth Observation Data Management System (EODMS). Note some collection names
    /// have hyphens in EODMS which cannot be used as enum names but are included in the description attribute.
    /// </summary>
    [Serializable()]
    public enum EodmsObservationCollections
    {
        [Description("NAPL")]
        NAPL,
        [Description("SGBAirPhotos")]
        SGBAirPhotos,
        [Description("RCMImageProducts")]
        RCMImageProducts,
        [Description("COSMO-SkyMed1")]
        COSMOSkyMed1,
        [Description("Radarsat1")]
        Radarsat1,
        [Description("Radarsat1RawProducts")]
        Radarsat1RawProducts,
        [Description("Radarsat2")]
        Radarsat2,
        [Description("Radarsat2RawProducts")]
        Radarsat2RawProducts,
        [Description("RCMScienceData")]
        RCMScienceData,
        [Description("TerraSarX")]
        TerraSarX,
        [Description("DMC")] 
        DMC,
        [Description("Gaofen-1")]
        Gaofen1,
        [Description("GeoEye-1")]
        GeoEye1,
        [Description("IKONOS")]
        IKONOS,
        [Description("IRS")]
        IRS,
        [Description("PlanetScope")]
        PlanetScope,
        [Description("QuickBird-2")]
        QuickBird2,
        [Description("RapidEye")]
        RapidEye,
        [Description("SPOT")]
        SPOT,
        [Description("WorldView-1")]
        WorldView1,
        [Description("WorldView-2")]
        WorldView2,
        [Description("WorldView-3")]
        WorldView3,
        [Description("VASP")]
        VASP
    }
}

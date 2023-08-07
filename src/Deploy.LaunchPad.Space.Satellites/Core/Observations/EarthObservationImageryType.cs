namespace Deploy.LaunchPad.Space.Satellites.Core
{
    /// <summary>
    /// Aligns to the NASA Earth Observing System Data and Information System (EOSDIS) data product levels.
    /// https://earthdata.nasa.gov/collaborate/open-data-services-and-software/data-information-policy/data-levels
    /// </summary>
    public enum EarthObservationImageryType
    {
        Panchromatic = 0,
        Multispectral = 1,
        PanSharpened = 2,
        Hyperspectral = 3,
        MicrowaveRadiometry = 4,
        SyntheticApertureRadar = 5,
        Lidar = 6,
        RadarAltimetry = 7,
        GNSSR = 8,
        RadarScatterometry = 9,
        Aerial = 10
    }
}

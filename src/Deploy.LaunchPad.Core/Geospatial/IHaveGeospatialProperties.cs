using Deploy.LaunchPad.Core.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Geospatial.H3;
using Deploy.LaunchPad.Core.Geospatial.Overture;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Geospatial
{
    public partial interface IHaveGeospatialProperties : 
        IMayHaveAltitude, 
        IMayHaveElevation,
        IMayHaveBoundingBox,
        IMayHaveGeoJsonDefinition,
        IMayHaveOvertureMapsLocation,
        IMayHaveH3Definition
    {

        ///<summary>
        /// Describes GPS location for the asset (ex 45.4201, -75.68775264 )
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public System.Double? Latitude { get; }

        ///<summary>
        /// Describes GPS Longitude for the asset (ex 45.4201, -75.68775264 )
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public System.Double? Longitude { get; }

    }
}

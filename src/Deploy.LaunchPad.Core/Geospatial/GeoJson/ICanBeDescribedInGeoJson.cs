﻿// Initially generated by quicktype https://github.com/quicktype/quicktype under the Apache 2 license.
// using the json schema found here: https://geojson.org/schema/GeoJSON.json
// 
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy.LaunchPad.Core.Geospatial.GeoJson
{
    public interface ICanBeDescribedInGeoJson<TGeoJsonType>
        where TGeoJsonType : Geometry
    {
        public TGeoJsonType GetGeometry();
        
        public string GeoJson { get; set; }

    }
}
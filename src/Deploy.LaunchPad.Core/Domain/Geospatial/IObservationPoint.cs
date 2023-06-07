﻿using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Geometries;
using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Types;
using Deploy.LaunchPad.Core.GeoJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Geography
{
    public partial interface IObservationPoint<TPrimaryKey, TParentAoiGeoJsonType> : ICanBeDescribedInGeoJson<Point>
        where TParentAoiGeoJsonType : GeoJsonGeometryTypeBase, new()
    {
        public IAreaOfInterest<TPrimaryKey, TParentAoiGeoJsonType> ParentAoi { get; set; }

    }
}
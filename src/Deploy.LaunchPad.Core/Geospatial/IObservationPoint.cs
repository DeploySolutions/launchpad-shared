// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IObservationPoint.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Geospatial.H3;
using NetTopologySuite.Geometries;

namespace Deploy.LaunchPad.Core.Geospatial
{
    /// <summary>
    /// Interface IObservationPoint
    /// Extends the <see cref="Deploy.LaunchPad.Core.Geospatial.IHaveGeographicPosition" />
    /// </summary>
    /// <typeparam name="TParentAreaOfInterest">The type of the t parent area of interest.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Geospatial.IHaveGeographicPosition" />
    public partial interface IObservationPoint<TParentAreaOfInterest> : IHaveGeographicPosition
        where TParentAreaOfInterest : IAreaOfInterest
    {
        /// <summary>
        /// Gets or sets the parent aoi.
        /// </summary>
        /// <value>The parent aoi.</value>
        public TParentAreaOfInterest ParentAoi { get; set; }

    }
}

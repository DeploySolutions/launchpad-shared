// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IAltitude.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Domain.Data;

namespace Deploy.LaunchPad.Domain.Geospatial.ReferencePoint
{
    public interface IAltitude : IMustHaveUnitOfMeasure, IMayHaveConfidence
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IArea.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************


// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IArea.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************



using Deploy.LaunchPad.Core.Metadata;

namespace Deploy.LaunchPad.Geospatial.Position
{
    public partial interface IArea : IMustHaveUnitOfMeasure, IMayHaveConfidence
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
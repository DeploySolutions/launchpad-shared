// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IMayHaveArea.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Deploy.LaunchPad.Core.Domain;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{

    /// <summary>
    /// This interface defines the physical Area of something
    /// </summary>
    public partial interface IMayHaveArea
    {

        /// <summary>
        /// Gets or sets the Area.
        /// </summary>
        /// <value>The Area.</value>
        public Area? Area { get; set; }

    }
}
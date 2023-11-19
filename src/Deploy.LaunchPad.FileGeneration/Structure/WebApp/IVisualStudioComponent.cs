// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-01-2023
// ***********************************************************************
// <copyright file="IVisualStudioComponent.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Deploy.LaunchPad.FileGeneration.Structure.WebApp;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface IVisualStudioComponent
    /// Extends the <see cref="Deploy.LaunchPad.FileGeneration.Structure.IHaveSoftwareInfrastructure" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.IHaveSoftwareInfrastructure" />
    public partial interface IVisualStudioComponent : IHaveSoftwareInfrastructure
    {
        /// <summary>
        /// Gets or sets the cors.
        /// </summary>
        /// <value>The cors.</value>
        public CorsSettings Cors { get; set; }
        /// <summary>
        /// Gets or sets the application services.
        /// </summary>
        /// <value>The application services.</value>
        public IDictionary<string, LaunchPadGeneratedApplicationService> ApplicationServices { get; set; }
        /// <summary>
        /// Gets or sets the domain entities.
        /// </summary>
        /// <value>The domain entities.</value>
        public IDictionary<string, LaunchPadGeneratedDomainEntity> DomainEntities { get; set; }

        /// <summary>
        /// Gets or sets the value objects.
        /// </summary>
        /// <value>The value objects.</value>
        public IDictionary<string, LaunchPadGeneratedValueObject> ValueObjects { get; set; }

        /// <summary>
        /// Checks the validity.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckValidity();
    }
}
// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Sentinel1ObservationScene.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Space.Satellites.Core;

namespace Deploy.LaunchPad.Space.Satellites.Sentinel
{
    /// <summary>
    /// Class Sentinel1ObservationScene.
    /// Implements the <see cref="EarthObservationModelBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.Sentinel.ISentinel1ObservationScene" />
    /// </summary>
    /// <seealso cref="EarthObservationModelBase" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.Sentinel.ISentinel1ObservationScene" />
    public partial class Sentinel1ObservationScene : EarthObservationModelBase,
        ISentinel1ObservationScene
    {
        /// <summary>
        /// Gets or sets the bucket.
        /// </summary>
        /// <value>The bucket.</value>
        public string Bucket { get; set; }
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public string ProductId { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>The type of the product.</value>
        public string ProductType { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sentinel1ObservationScene"/> class.
        /// </summary>
        public Sentinel1ObservationScene()
        {

        }

    }
}

// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Landsat8ObservationScene.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Files.Storage;
using Deploy.LaunchPad.Space.Satellites.Core;
using Deploy.LaunchPad.Files.Storage;

namespace Deploy.LaunchPad.Space.Satellites.Landsat
{
    /// <summary>
    /// Class Landsat8ObservationScene.
    /// Implements the <see cref="EarthObservationModelBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.Landsat.ILandsatObservationScene" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TFileStorageLocationType">The type of the t file storage location type.</typeparam>
    /// <seealso cref="EarthObservationModelBase" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.Landsat.ILandsatObservationScene" />
    public partial class Landsat8ObservationScene<TPrimaryKey, TFileStorageLocationType> : EarthObservationModelBase,
        ILandsatObservationScene
        where TFileStorageLocationType : IFileStorageLocation, new()
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
        /// Gets or sets the collection.
        /// </summary>
        /// <value>The collection.</value>
        public string Collection { get; set; }
        /// <summary>
        /// Gets or sets the projection.
        /// </summary>
        /// <value>The projection.</value>
        public string Projection { get; set; }
        /// <summary>
        /// Gets or sets the name of the sensor.
        /// </summary>
        /// <value>The name of the sensor.</value>
        public string SensorName { get; set; }
        /// <summary>
        /// Gets or sets the year acquired.
        /// </summary>
        /// <value>The year acquired.</value>
        public string YearAcquired { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public string Row { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Landsat8ObservationScene{TPrimaryKey, TFileStorageLocationType}"/> class.
        /// </summary>
        public Landsat8ObservationScene()
        {

        }

    }
}

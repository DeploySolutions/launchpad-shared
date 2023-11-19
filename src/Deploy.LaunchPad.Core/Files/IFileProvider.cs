// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IFileProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain
{
    /// <summary>
    /// Interface IFileProvider
    /// </summary>
    public partial interface IFileProvider
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        [NotMapped]
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Description { get; set; }

        /// <summary>
        /// Contains a dictionary of file locations, keyed using the friendly name of the location.
        /// Each location contains the instruct
        /// </summary>
        /// <value>The locations.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public Dictionary<string, IFileStorageLocation> Locations { get; set; }

        // get methods
        /// <summary>
        /// Gets the location by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IFileStorageLocation.</returns>
        public IFileStorageLocation GetLocationById(string id, string caller);

        /// <summary>
        /// Gets the location by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;IFileStorageLocation&gt;.</returns>
        public Task<IFileStorageLocation> GetLocationByIdAsync(string id, string caller);


    }
}

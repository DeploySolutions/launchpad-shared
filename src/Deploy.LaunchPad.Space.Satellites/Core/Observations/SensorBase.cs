// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="SensorBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    /// <summary>
    /// Class SensorBase.
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.Core.Observations.ISensor" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.Core.Observations.ISensor" />
    [Serializable]
    public abstract partial class SensorBase : ISensor
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual ElementName Name { get; set; }

        /// <summary>
        /// Gets or sets the supported imagery types.
        /// </summary>
        /// <value>The supported imagery types.</value>
        [Required]
        public virtual IDictionary<string, EarthObservationImageryType> SupportedImageryTypes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorBase"/> class.
        /// </summary>
        protected SensorBase()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            SupportedImageryTypes = new Dictionary<string, EarthObservationImageryType>(comparer);
        }
    }
}

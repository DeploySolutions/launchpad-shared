// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ICanBeLaunchPadBackgroundJobArgs.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Deploy.LaunchPad.Core.BackgroundProcess
{
    //
    // Summary:
    //     This interface is used to mark classes that can be run as LaunchPad background job arguments
    //
    // Type parameters:
    //   TArgs:
    /// <summary>
    /// Interface ICanBeLaunchPadBackgroundJobArgs
    /// </summary>
    public partial interface ICanBeLaunchPadBackgroundJobArgs
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description short.
        /// </summary>
        /// <value>The description short.</value>
        public string DescriptionShort { get; set; }

        /// <summary>
        /// Gets or sets the description full.
        /// </summary>
        /// <value>The description full.</value>
        public string DescriptionFull { get; set; }

        /// <summary>
        /// Gets or sets the chron.
        /// </summary>
        /// <value>The chron.</value>
        public string Chron { get; set; }

    }
}

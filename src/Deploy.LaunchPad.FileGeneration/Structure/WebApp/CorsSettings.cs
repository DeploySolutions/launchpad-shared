// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-01-2023
// ***********************************************************************
// <copyright file="CorsSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.WebApp
{
    /// <summary>
    /// Class CorsSettings.
    /// </summary>
    [Serializable]
    public partial class CorsSettings
    {
        /// <summary>
        /// Gets or sets the allowed hosts.
        /// </summary>
        /// <value>The allowed hosts.</value>
        public virtual IDictionary<string, string> AllowedHosts { get; set; }

        /// <summary>
        /// Gets or sets the allowed methods.
        /// </summary>
        /// <value>The allowed methods.</value>
        public virtual IDictionary<string, string> AllowedMethods { get; set; }

        /// <summary>
        /// Gets or sets the access control allow headers.
        /// </summary>
        /// <value>The access control allow headers.</value>
        public virtual IDictionary<string, string> AccessControlAllowHeaders { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [access control allow credentials].
        /// </summary>
        /// <value><c>true</c> if [access control allow credentials]; otherwise, <c>false</c>.</value>
        public virtual bool AccessControlAllowCredentials { get; set; } = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="CorsSettings"/> class.
        /// </summary>
        public CorsSettings()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            AllowedHosts = new Dictionary<string, string>(comparer)
            {
                { "[PUT SOMETHING HERE, DO NOT USE * ]", "[PUT SOMETHING HERE, DO NOT USE * ]" }
            };
            

            AllowedMethods = new Dictionary<string, string>(comparer)
            {
                { "GET", "GET" },
                { "OPTIONS", "OPTIONS" }
            };

            AccessControlAllowHeaders = new Dictionary<string, string>(comparer)
            {
                { "Accept", "Accept" }
            };

        }
    }
}

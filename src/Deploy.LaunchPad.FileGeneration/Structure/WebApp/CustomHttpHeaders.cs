// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-23-2023
// ***********************************************************************
// <copyright file="CustomHttpHeaders.cs" company="Deploy Software Solutions, inc.">
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
    /// Class CustomHttpHeaders.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.WebApp.ICustomHttpHeaders" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.WebApp.ICustomHttpHeaders" />
    [Serializable()]
    public partial class CustomHttpHeaders : ICustomHttpHeaders
    {

        /// <summary>
        /// Contains a dictionary of the custom headers to add, ensuring each can only be added once. Note the headers may already exist elsewhere in some code.
        /// </summary>
        /// <value>The custom HTTP headers to add.</value>
        public virtual IDictionary<string, string> CustomHttpHeadersToAdd { get; set; }

        /// <summary>
        /// Contains a hashset of the custom headers to remove. Note the headers may already exist elsewhere in some code.
        /// </summary>
        /// <value>The custom HTTP headers to remove.</value>
        public virtual HashSet<string> CustomHttpHeadersToRemove { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHttpHeaders"/> class.
        /// </summary>
        public CustomHttpHeaders()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomHttpHeadersToAdd = new Dictionary<string, string>(comparer);
            CustomHttpHeadersToRemove = new HashSet<string>(comparer);

        }

    }
}

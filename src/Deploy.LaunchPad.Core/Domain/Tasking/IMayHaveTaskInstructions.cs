using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Tasking
{
    public partial interface IMayHaveTaskInstructions
    {
        /// <summary>
         /// Gets or sets the instructions short.
         /// </summary>
         /// <value>The instructions short.</value>
        public string? Short { get; set; }
        /// <summary>
        /// Gets or sets the full instructions.
        /// </summary>
        /// <value>The full instructions.</value>
        public string? Full { get; set; }

        /// <summary>
        /// Gets or sets the more information URI.
        /// </summary>
        /// <value>The more information URI.</value>
        public Uri? MoreInformationUri { get; set; }
    }
}

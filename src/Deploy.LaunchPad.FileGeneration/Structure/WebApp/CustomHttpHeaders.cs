using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.WebApp
{
    [Serializable()]
    public partial class CustomHttpHeaders : ICustomHttpHeaders
    {

        /// <summary>
        /// Contains a dictionary of the custom headers to add, ensuring each can only be added once. Note the headers may already exist elsewhere in some code.
        /// </summary>
        public virtual IDictionary<string, string> CustomHttpHeadersToAdd { get; set; }

        /// <summary>
        /// Contains a dictionary of the custom headers to remove, ensuring each can only be added once. Note the headers may already exist elsewhere in some code.
        /// </summary>
        public virtual IDictionary<string, string> CustomHttpHeadersToRemove { get; set; }

        public CustomHttpHeaders()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomHttpHeadersToAdd = new Dictionary<string, string>(comparer);
            CustomHttpHeadersToRemove = new Dictionary<string, string>(comparer);

        }

    }
}

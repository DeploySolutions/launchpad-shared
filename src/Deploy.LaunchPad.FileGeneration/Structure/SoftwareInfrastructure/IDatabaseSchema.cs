using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface IDatabaseSchema
    {
        /// <summary>
        /// Gets or sets the schema name.
        /// </summary>
        /// <value>The default schema.</value>
        public string Name { get; set; }

        public IDictionary<string, IDatabaseTable> Tables { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface IDatabaseTable
    {
        /// <summary>
        /// Gets or sets the default database name.
        /// </summary>
        /// <value>The default schema.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        /// <value>The default schema.</value>
        public string SchemaName { get; set; }


    }
}

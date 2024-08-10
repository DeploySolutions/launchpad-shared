using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface IDatabase
    {
        /// <summary>
        /// Gets or sets the default database name.
        /// </summary>
        /// <value>The default schema.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default schema.
        /// </summary>
        /// <value>The default schema.</value>
        public string DefaultSchema { get; set; }

        public IDictionary<string,IDatabaseSchema> Schemas { get; set; }

        /// <summary>
        /// Gets or sets the database engine.
        /// </summary>
        /// <value>The database engine.</value>
        public string Engine { get; set; }

        /// <summary>
        /// Gets or sets the version, if the database engine has multiple syntaxes or versions
        /// </summary>
        public string Version { get; set; }
    }
}

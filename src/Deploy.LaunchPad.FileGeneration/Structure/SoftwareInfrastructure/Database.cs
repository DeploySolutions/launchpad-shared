using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class Database: IDatabase
    {
        /// <summary>
        /// Gets or sets the schema name.
        /// </summary>
        /// <value>The default schema.</value>
        public virtual string Name { get; set; }
        public virtual string DefaultSchema { get;set; }
        public virtual IDictionary<string, IDatabaseSchema> Schemas { get; set; }
        public virtual string Engine { get; set; }
        public virtual string Version { get; set; }

        public Database()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Schemas = new Dictionary<string, IDatabaseSchema>(comparer);
        }
    }
}

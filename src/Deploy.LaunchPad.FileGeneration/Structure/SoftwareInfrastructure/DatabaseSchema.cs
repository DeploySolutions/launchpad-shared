using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class DatabaseSchema : IDatabaseSchema
    {
        /// <summary>
        /// Gets or sets the schema name.
        /// </summary>
        /// <value>The default schema.</value>
        public virtual string Name { get; set; }

        public virtual IDictionary<string, IDatabaseTable> Tables { get; set; }

        public DatabaseSchema()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tables = new Dictionary<string, IDatabaseTable>(comparer);

        }

    }
}

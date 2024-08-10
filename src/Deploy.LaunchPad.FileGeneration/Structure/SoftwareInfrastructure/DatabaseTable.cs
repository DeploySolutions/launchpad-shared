using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class DatabaseTable: IDatabaseTable
    {
        /// <summary>
        /// Gets or sets the schema name.
        /// </summary>
        /// <value>The default schema.</value>
        public virtual string Name { get; set; }
        public virtual string SchemaName { get;set; }

        public DatabaseTable()
        {
        }
    }
}

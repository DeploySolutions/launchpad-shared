using Deploy.LaunchPad.Util.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface IMustHaveSchemaDotOrgJson<TSchemaDotOrg>
    {
        public TSchemaDotOrg? SchemaType { get; set; }

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string? SchemaDotOrgJson { get; set; }

        public ILaunchPadSchemaDetails<JsonToken>? SchemaDetails { get; set; }
    }
}

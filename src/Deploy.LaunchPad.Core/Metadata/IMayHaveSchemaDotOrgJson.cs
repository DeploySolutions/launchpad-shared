using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMayHaveSchemaDotOrgJson
    {

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string? SchemaDotOrgJson { get; set; }

        public string? EntityType { get; set; }

        public ILaunchPadSchemaDetails<JsonToken>? SchemaDetails { get; set; }
    }
}

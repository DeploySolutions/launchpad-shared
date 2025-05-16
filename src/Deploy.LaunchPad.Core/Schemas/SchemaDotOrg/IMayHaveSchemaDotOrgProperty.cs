using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Schemas.SchemaDotOrg
{
    public partial interface IMayHaveSchemaDotOrgProperty<TSchema>
       where TSchema : Schema.NET.IThing
    {
        public string SchemaDotOrgJson { get; set; }
    }
}

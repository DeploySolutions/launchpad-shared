using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class SqlFile : FileBase<string>, ISqlFile
    {
        public override string Extension => ".sql";
        public virtual bool IsTransaction { get; set; } = false;
        public string? DatabaseName { get; set; }
        public string? DatabaseSchema { get; set; }
        public virtual string DatabaseTableName { get; set; }
        public virtual SqlScriptType ScriptType { get; set; } = SqlScriptType.INSERT; // Can be INSERT, UPDATE, DELETE

    }

    public enum SqlScriptType
    {
        INSERT,
        UPDATE,
        DELETE,
        MIXED
    }
}

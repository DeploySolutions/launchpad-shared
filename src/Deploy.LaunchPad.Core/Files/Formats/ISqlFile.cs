﻿using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface ISqlFile : IFile<string>
    {
        string DatabaseName { get; set; }
        string DatabaseSchema { get; set; }
        string DatabaseTableName { get; set; }
        bool IsTransaction { get; set; }
        SqlScriptType ScriptType { get; set; }
    }
}
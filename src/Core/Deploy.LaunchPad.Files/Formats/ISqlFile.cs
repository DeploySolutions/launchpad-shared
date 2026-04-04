using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface ISqlFile : IFile<string, SqlFileSchema>
    {
        string DatabaseName { get; set; }
        string DatabaseSchema { get; set; }
        string DatabaseTableName { get; set; }
        bool IsTransaction { get; set; }
        SqlScriptType ScriptType { get; set; }
    }
}
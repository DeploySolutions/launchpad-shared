using Castle.Core.Logging;

namespace Deploy.LaunchPad.Python
{
    public partial interface IPythonScriptService
    {
        ILogger Logger { get; set; }
        IPythonInstallation Python { get; set; }
        IPythonScript Script { get; set; }

        string GetTextFromScript(string args);
    }
}
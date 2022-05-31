using Castle.Core.Logging;

namespace DeploySoftware.LaunchPad.Python
{
    public partial interface IPythonScriptService
    {
        ILogger Logger { get; set; }
        PythonInstallation Python { get; set; }
        PythonScript Script { get; set; }

        string GetTextFromScript(string args);
    }
}
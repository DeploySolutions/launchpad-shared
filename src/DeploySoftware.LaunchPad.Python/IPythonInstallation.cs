using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Python
{
    public interface IPythonInstallation
    {
        string InstallationFilePath { get; set; }
        PythonMajorVersion MajorVersion { get; set; }
        PythonMinorVersion MinorVersion { get; set; }
        IDictionary<string, string> ModuleFilePaths { get; set; }
    }
}
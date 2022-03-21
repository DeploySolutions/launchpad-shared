using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Python
{
    [Serializable()]
    public partial class PythonInstallation
    {
        public virtual PythonMajorVersion MajorVersion { get; set; }

        public virtual PythonMinorVersion MinorVersion { get; set; }

        public string InstallationFilePath { get; set; } 

        public IDictionary<string,string> ModuleFilePaths { get; set; }

        protected PythonInstallation()
        {
            MajorVersion = PythonMajorVersion.Three;
            MinorVersion = PythonMinorVersion.Nine;
            var comparer = StringComparer.OrdinalIgnoreCase;
            ModuleFilePaths = new Dictionary<string, string>(comparer);
        }

        public PythonInstallation(string installationFilePath, PythonMajorVersion majorVersion, PythonMinorVersion minorVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            InstallationFilePath = installationFilePath; 
            var comparer = StringComparer.OrdinalIgnoreCase;            
            ModuleFilePaths = new Dictionary<string, string>(comparer);
        }

        public PythonInstallation(string installationFilePath, PythonMajorVersion majorVersion, PythonMinorVersion minorVersion, IDictionary<string, string> moduleFilePaths)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            InstallationFilePath = installationFilePath;
            ModuleFilePaths = moduleFilePaths;
        }

        public PythonInstallation(string installationFilePath, IDictionary<string, string> moduleFilePaths, PythonMajorVersion majorVersion, PythonMinorVersion minorVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            InstallationFilePath = installationFilePath;
            ModuleFilePaths = moduleFilePaths;
        }
    }
}

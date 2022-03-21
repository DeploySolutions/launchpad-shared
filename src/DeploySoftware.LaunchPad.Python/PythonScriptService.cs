using Abp.Dependency;
using Castle.Core.Logging;
using System;
using System.Diagnostics;
using System.IO;
using DeploySoftware.LaunchPad.Core.Util;
using DeploySoftware.LaunchPad.Core.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Python
{
    public partial class PythonScriptService : ISystemIntegrationService
    {
        public virtual ILogger Logger { get; set; }

        public PythonScript Script { get; set; }

        public PythonInstallation Python { get; set; }

        protected PythonScriptService()
        {
            Logger = NullLogger.Instance;
        }

        public PythonScriptService(ILogger logger, string pythonInstallationFilePath, string scriptFileName)
        {
            Logger = logger; 
            Python = new PythonInstallation(pythonInstallationFilePath, PythonMajorVersion.Three, PythonMinorVersion.Eight);
            Script = new PythonScript(scriptFileName);
        }

        public PythonScriptService(ILogger logger, string pythonInstallationFilePath, string scriptFileName, IDictionary<string,string> moduleFilePaths)
        {
            Logger = logger;
            Python = new PythonInstallation(pythonInstallationFilePath, PythonMajorVersion.Three, PythonMinorVersion.Eight, moduleFilePaths);
            Script = new PythonScript(scriptFileName);
        }

        public string GetTextFromScript(string args)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(Script.FileName), "Python script filename must not be empty");
            Logger.Info("GetTextFromScript() started.");
            
            string cmd = Script.FileName; 
            if (!string.IsNullOrEmpty(Script.FolderPath))
            {
                if(!Script.FolderPath.EndsWith(Path.DirectorySeparatorChar))
                {
                    Script.FolderPath += Path.DirectorySeparatorChar;
                }
                cmd.Insert(0, Script.FolderPath); // add the folder to the beginning
            }
            
            string scriptResult = string.Empty;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Python.InstallationFilePath;
            start.Arguments = string.Format("\"{0}\" {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    try
                    {
                        scriptResult = reader.ReadToEnd();
                        Logger.Info(string.Format("Script result succeeded, returned '{0}'",scriptResult));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                    }
                }
            }
            Logger.Info("GetTextFromScript() ended.");
            return scriptResult;
        }

    }
}

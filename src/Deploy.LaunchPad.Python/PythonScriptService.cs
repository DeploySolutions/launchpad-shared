using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Application;
using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Deploy.LaunchPad.Python
{
    public partial class PythonScriptService : ISystemIntegrationService, IPythonScriptService
    {
        public virtual ILogger Logger { get; set; }

        public IPythonScript Script { get; set; }

        public IPythonInstallation Python { get; set; }

        protected PythonScriptService()
        {
            Logger = NullLogger.Instance;
        }

        public PythonScriptService(ILogger logger, Uri pythonInstallationFilePath, string scriptFileName)
        {
            Logger = logger;
            Python = new PythonInstallation(pythonInstallationFilePath, PythonMajorVersion.Three, PythonMinorVersion.Eight);
            Script = new PythonScript(scriptFileName);
        }

        public PythonScriptService(ILogger logger, Uri pythonInstallationFilePath, string scriptFileName, IDictionary<string, Uri> moduleFilePaths)
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
            Logger.Debug(string.Format("GetTextFromScript(), cmd is '{0}'", cmd));

            string scriptResult = string.Empty;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Python.InstallLocation.ToString();
            Logger.Debug(string.Format("GetTextFromScript(), Python.InstallationFilePath is '{0}'", Python.InstallLocation));
            start.Arguments = string.Format("{0} {1}", cmd, args);
            Logger.Debug(string.Format("GetTextFromScript(), args is '{0}'", args));
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    try
                    {
                        scriptResult = reader.ReadToEnd();
                        Logger.Info(string.Format("Script result succeeded, returned '{0}'", scriptResult));
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

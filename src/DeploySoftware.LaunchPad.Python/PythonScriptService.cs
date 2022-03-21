using Abp.Dependency;
using Castle.Core.Logging;
using System;
using System.Diagnostics;
using System.IO;
using DeploySoftware.LaunchPad.Core.Util;
using DeploySoftware.LaunchPad.Core.Configuration;

namespace DeploySoftware.LaunchPad.Python
{
    public class PythonScriptService : ISystemIntegrationService
    {
        public virtual ILogger Logger { get; set; }

        public string ScriptFolderPath { get; set; } = string.Empty;

        public string ScriptFileName { get; set; } = string.Empty;

        public string PythonInstallationFilePath { get; set; } = string.Empty;

        public PythonVersion PythonVersion { get; set; } = PythonVersion.V3_9;

        public PythonScriptService()
        {
            Logger = NullLogger.Instance;
        }

        public PythonScriptService(ILogger logger)
        {
            Logger = logger;
        }

        public string GetTextFromScript(string args)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(ScriptFileName), "Python script filename must not be empty");
            Logger.Info("GetTextFromScript() started.");
            if(!ScriptFileName.EndsWith(".py"))
            {
                ScriptFileName += ".py";
            }
            string cmd = ScriptFileName; 
            if (!string.IsNullOrEmpty(ScriptFolderPath))
            {
                if(!ScriptFolderPath.EndsWith(Path.DirectorySeparatorChar))
                {
                    ScriptFolderPath += Path.DirectorySeparatorChar;
                }
                cmd.Insert(0, ScriptFolderPath); // add the folder to the beginning
            }
            
            string scriptResult = string.Empty;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = PythonInstallationFilePath;
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

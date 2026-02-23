// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="PythonScriptService.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Domain;
using Deploy.LaunchPad.Code.Services;
using Deploy.LaunchPad.Code.Config;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Class PythonScriptService.
    /// Implements the <see cref="ILaunchPadSystemIntegrationService" />
    /// Implements the <see cref="Deploy.LaunchPad.Python.IPythonScriptService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    /// <seealso cref="Deploy.LaunchPad.Python.IPythonScriptService" />
    public partial class PythonScriptService : LaunchPadServiceBase, ILaunchPadSystemIntegrationService, IPythonScriptService
    {
        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public IPythonScript Script { get; set; }

        /// <summary>
        /// Gets or sets the python.
        /// </summary>
        /// <value>The python.</value>
        public IPythonInstallation Python { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonScriptService"/> class.
        /// </summary>
        protected PythonScriptService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonScriptService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pythonInstallationFilePath">The python installation file path.</param>
        /// <param name="scriptFileName">Name of the script file.</param>
        public PythonScriptService(ILogger logger, Uri pythonInstallationFilePath, string scriptFileName) : base(logger)
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("PythonScriptService {0} ", id));
            Description = new ElementDescription(string.Format("PythonScriptService {0} ", id));
            Python = new PythonInstallation(pythonInstallationFilePath, PythonMajorVersion.Three, PythonMinorVersion.Eight, 16);
            Script = new PythonScript(scriptFileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonScriptService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="pythonInstallationFilePath">The python installation file path.</param>
        /// <param name="scriptFileName">Name of the script file.</param>
        /// <param name="moduleFilePaths">The module file paths.</param>
        public PythonScriptService(ILogger logger, Uri pythonInstallationFilePath, string scriptFileName, IDictionary<string, Uri> moduleFilePaths)
            : base(logger)
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("PythonScriptService {0} ", id));
            Description = new ElementDescription(string.Format("PythonScriptService {0} ", id));
            Python = new PythonInstallation(pythonInstallationFilePath, PythonMajorVersion.Three, PythonMinorVersion.Eight, 16, moduleFilePaths);
            Script = new PythonScript(scriptFileName);
        }

        /// <summary>
        /// Gets the text from script.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>System.String.</returns>
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

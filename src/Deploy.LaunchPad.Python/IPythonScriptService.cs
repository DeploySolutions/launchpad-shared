// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IPythonScriptService.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Services;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Interface IPythonScriptService
    /// </summary>
    public partial interface IPythonScriptService : ILaunchPadService
    {
        
        /// <summary>
        /// Gets or sets the python.
        /// </summary>
        /// <value>The python.</value>
        IPythonInstallation Python { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        IPythonScript Script { get; set; }

        /// <summary>
        /// Gets the text from script.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>System.String.</returns>
        string GetTextFromScript(string args);
    }
}
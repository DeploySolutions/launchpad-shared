// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="PythonScript.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Class PythonScript.
    /// Implements the <see cref="Deploy.LaunchPad.Python.IPythonScript" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Python.IPythonScript" />
    [Serializable()]
    public partial class PythonScript : IPythonScript
    {
        /// <summary>
        /// Gets or sets the folder path.
        /// </summary>
        /// <value>The folder path.</value>
        public string FolderPath { get; set; } = string.Empty;

        /// <summary>
        /// The file name
        /// </summary>
        private string _fileName = string.Empty;
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (!value.EndsWith(".py"))
                {
                    _fileName = value + ".py";
                }
                else
                {
                    _fileName = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonScript"/> class.
        /// </summary>
        protected PythonScript()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonScript"/> class.
        /// </summary>
        /// <param name="scriptFilePath">The script file path.</param>
        public PythonScript(string scriptFilePath)
        {
            FileName = scriptFilePath;
            FolderPath = new FileInfo(scriptFilePath).Directory.FullName;
        }

    }
}

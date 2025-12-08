using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PowershellFile : FileBase<string, PowershellFileSchema>, IPowershellFile
    {
        public override string Extension => "." + FileExtensions.ps1;

        /// <summary>
        /// Constructor
        /// </summary>
        public PowershellFile(string fileName) : base(fileName)
        {
        }

    }
}

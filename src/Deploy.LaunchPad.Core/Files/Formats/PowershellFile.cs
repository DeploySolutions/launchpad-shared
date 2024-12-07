using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PowershellFile : FileBase<string>, IPowershellFile
    {

        public override string Extension => ".ps1";

        /// <summary>
        /// Constructor
        /// </summary>
        public PowershellFile(string fileName) : base(fileName)
        {
        }

    }
}

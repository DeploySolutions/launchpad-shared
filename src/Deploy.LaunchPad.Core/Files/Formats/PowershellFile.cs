using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PowershellFile : FileBase<string>, IPowershellFile
    {

        public override string Extension => ".ps1";

        public PowershellFile()
        {
        }
    }
}

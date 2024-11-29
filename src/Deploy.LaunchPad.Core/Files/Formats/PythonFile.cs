using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PythonFile : FileBase<string>, IPythonFile
    {

        public override string Extension => ".py";

        public PythonFile()
        {
        }
    }
}

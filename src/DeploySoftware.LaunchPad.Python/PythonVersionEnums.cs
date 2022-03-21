using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Python
{
    public enum PythonMajorVersion
    {
        [Description("4")]
        Four,
        [Description("3")]
        Three,
        [Description("2")]
        Two
    }

    public enum PythonMinorVersion
    {
        [Description("9")]
        Nine,
        [Description("8")]
        Eight,
        [Description("7")]
        Seven,
        [Description("6")]
        Six,
        [Description("5")]
        Five,
        [Description("4")]
        Four,
        [Description("3")]
        Three,
        [Description("2")]
        Two,
        [Description("1")]
        One,
        [Description("0")]
        Zero

    }

}

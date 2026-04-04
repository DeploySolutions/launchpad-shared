using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.CommandLine
{
    public enum OptionArity
    {
        None, // flag (bool switch) e.g., --yell
        One   // requires a value e.g., --name Nicholas
    }
}

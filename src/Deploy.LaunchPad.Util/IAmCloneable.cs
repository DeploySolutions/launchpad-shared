﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util
{
    public interface IAmCloneable<out T>
    {
        T CloneGeneric();
    }
}

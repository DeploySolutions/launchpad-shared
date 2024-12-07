﻿using Deploy.LaunchPad.Core.Util;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface ITokenizedFile : IFile<string>
    {
        IDictionary<string, LaunchPadToken> Tokens { get; set; }
    }
}
﻿using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface IJupyterNotebookFile : IFile<byte[], JToken>
    {
    }
}
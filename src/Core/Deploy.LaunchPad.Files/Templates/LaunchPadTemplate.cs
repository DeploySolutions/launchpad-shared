using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace Deploy.LaunchPad.Files.Templates
{
    public partial class LaunchPadTemplate : FileBase<string, JsonObject>, ILaunchPadTemplate
    {
        public LaunchPadTemplate(string fileName) : base(fileName)
        {
        }
    }
}

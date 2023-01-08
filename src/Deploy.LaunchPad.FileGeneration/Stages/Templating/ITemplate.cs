using Deploy.LaunchPad.Core.Util;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    public interface ITemplate
    {
        IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }
        string Key { get; set; }
        string Name { get; set; }
        string TemplateBasePath { get; set; }

        bool Equals(TemplateBase other);
    }
}
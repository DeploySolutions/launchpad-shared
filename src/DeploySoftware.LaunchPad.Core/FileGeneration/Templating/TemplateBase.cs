
using DeploySoftware.LaunchPad.Core.Util;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class TemplateBase
    {
        public string Name { get; set; }

        public virtual string Key { get; set; }

        public virtual string TemplateBasePath { get; set; }
        public IList<LaunchPadToken> AvailableTokens { get; set; }

        public TemplateBase()
        {
            Name = string.Empty;
            Key = string.Empty;
            TemplateBasePath = string.Empty;
            AvailableTokens = new List<LaunchPadToken>();
        }

        public TemplateBase(string key, string templateBasePath)
        {
            Name = key;
            Key = key;
            TemplateBasePath = templateBasePath;
            AvailableTokens = new List<LaunchPadToken>();
        }

        public TemplateBase(string key, string templateBasePath, List<LaunchPadToken> tokens)
        {
            Name = key;
            Key = key;
            TemplateBasePath = templateBasePath;
            AvailableTokens = tokens;
        }

        public virtual bool Equals(TemplateBase other)
        {
            return this.Name.Equals(other.Name) &&
                   this.Key.Equals(other.Key);
        }

    }
}

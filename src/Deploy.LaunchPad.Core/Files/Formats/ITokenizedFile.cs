using Deploy.LaunchPad.Util;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface ITokenizedFile<TFileSchema> : IFile<string, TFileSchema>
    {
        IDictionary<string, LaunchPadToken> Tokens { get; set; }
    }
}
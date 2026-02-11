using Deploy.LaunchPad.Util.Tokens;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Util.Files
{
    public partial interface ITokenizedFile<TFileSchema> : IFile<string, TFileSchema>
    {
        IDictionary<string, LaunchPadToken> Tokens { get; set; }
    }
}
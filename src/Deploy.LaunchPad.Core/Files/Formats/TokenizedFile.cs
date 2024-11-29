using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class TokenizedFile : FileBase<string>, ITokenizedFile
    {

        public virtual IDictionary<string, LaunchPadToken> Tokens { get; set; }

        public TokenizedFile()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tokens = new Dictionary<string, LaunchPadToken>(comparer);
        }
    }
}

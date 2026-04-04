
using Deploy.LaunchPad.Util.Tokens;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial class TokenizedFile<TFileSchema> : FileBase<string, TFileSchema>, ITokenizedFile<TFileSchema>
    {
        public override string Extension => "." + FileExtension.rad + ".*";
        public virtual IDictionary<string, LaunchPadToken> Tokens { get; set; }

        public TokenizedFile(string fileName) : base(fileName)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tokens = new Dictionary<string, LaunchPadToken>(comparer);
        }
    }
}

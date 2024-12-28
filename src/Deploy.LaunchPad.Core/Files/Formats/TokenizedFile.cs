﻿using Deploy.LaunchPad.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class TokenizedFile<TFileSchema> : FileBase<string, TFileSchema>, ITokenizedFile<TFileSchema>
    {

        public virtual IDictionary<string, LaunchPadToken> Tokens { get; set; }

        public TokenizedFile(string fileName) : base(fileName)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tokens = new Dictionary<string, LaunchPadToken>(comparer);
        }
    }
}

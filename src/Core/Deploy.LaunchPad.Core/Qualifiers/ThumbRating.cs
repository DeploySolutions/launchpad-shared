using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Deploy.LaunchPad.Core.Qualifiers
{
    public enum ThumbRating
    {
        [Description("None")]
        None = 0,
        [Description("Thumbs Up")]
        ThumbsUp = 1,
        [Description("Thumbs Down")]
        ThumbsDown = 2
    }
}

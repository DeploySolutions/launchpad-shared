using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Util.Tokens
{
    /// <summary>
    /// Token matching can be done in two ways. By default, the token's value is ignored when checking for a match. 
    /// If you want to consider (require) the token's value is present when checking for a match, set this to ConsiderTokenValuesWhenMatching.
    /// </summary>
    public enum TokenMatchingStrategy
    {
        IgnoreTokenValuesWhenMatching = 0, // when checking for token matches, ignore what's currently in the token's Value field. (the default)
        ConsiderTokenValuesWhenMatching = 1, // when checking for token matches, only consider a match if what is in the token's Value field is also present in the token in the source text.        
    }

    /// <summary>
    /// Token matching can generate a lot of logging noise and incur processing overhead. By default, logging is disabled. 
    /// It can be enabled if you want to verify unexpected matching results or errors. If enabled, it uses the standard logging engine configuration.
    /// </summary>
    public enum TokenLoggingStrategy
    {
        DoNotLogTokenMatching = 0, // do not log any token matching activities. Calling methods can do any logging they need. (the default)
        LogTokenMatching = 1 // log all token matching activities 


    }
}

using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure.WebApp
{
    public partial interface ICustomHttpHeaders
    {
        IDictionary<string, string> CustomHttpHeadersToAdd { get; set; }
        HashSet<string> CustomHttpHeadersToRemove { get; set; }
    }
}
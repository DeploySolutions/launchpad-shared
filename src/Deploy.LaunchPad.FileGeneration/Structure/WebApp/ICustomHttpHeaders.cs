using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure.WebApp
{
    public partial interface ICustomHttpHeaders
    {
        IDictionary<string, string> CustomHttpHeadersToAdd { get; set; }
        IDictionary<string, string> CustomHttpHeadersToRemove { get; set; }
    }
}
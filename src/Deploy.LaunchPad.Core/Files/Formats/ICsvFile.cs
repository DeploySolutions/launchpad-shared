using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface ICsvFile : IFile<string>
    {
        string Delimiter { get; set; }
        bool IsHeaderCaseSensitive { get; set; }
        IList<string> PropertySortOrder { get; set; }
        char Quote { get; set; }
    }
}
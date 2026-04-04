using System.Collections.Generic;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface ICsvFile : IFile<string, IFrictionlessFileSchema>
    {
        string Delimiter { get; set; }
        bool IsHeaderCaseSensitive { get; set; }
        IList<string> PropertySortOrder { get; set; }
        char Quote { get; set; }
    }
}
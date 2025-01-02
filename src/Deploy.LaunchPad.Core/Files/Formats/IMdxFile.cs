using Deploy.LaunchPad.Core.Files.Formats;

namespace Deploy.LaunchPad.Core.Files
{
    /// <summary>
    /// MDX (Markdown for Components)
    /// </summary>
    public partial interface IMdxFile : IFile<string, MdxSchema>
    {
    }
}
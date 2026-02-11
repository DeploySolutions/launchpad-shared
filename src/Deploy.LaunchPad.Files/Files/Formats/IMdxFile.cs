

using Deploy.LaunchPad.Files.Formats;

namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// MDX (Markdown for Components)
    /// </summary>
    public partial interface IMdxFile : IFile<string, MdxFileSchema>
    {
    }
}
using Deploy.LaunchPad.Util.Files.Formats;

namespace Deploy.LaunchPad.Util.Files
{
    /// <summary>
    /// MDX (Markdown for Components)
    /// </summary>
    public partial interface IMdxFile : IFile<string, MdxFileSchema>
    {
    }
}
using Deploy.LaunchPad.Util.Files.Formats;

namespace Deploy.LaunchPad.Util.Files
{
    /// <summary>
    /// Toon (Token-Oriented Object Notation)
    /// </summary>
    public partial interface IToonFile : IFile<string, ToonFileSchema>
    {
    }
}
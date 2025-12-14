using Deploy.LaunchPad.Core.Files.Formats;

namespace Deploy.LaunchPad.Core.Files
{
    /// <summary>
    /// Toon (Token-Oriented Object Notation)
    /// </summary>
    public partial interface IToonFile : IFile<string, ToonFileSchema>
    {
    }
}
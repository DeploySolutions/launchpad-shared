using Deploy.LaunchPad.Files.Formats;


namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// Toon (Token-Oriented Object Notation)
    /// </summary>
    public partial interface IToonFile : IFile<string, ToonFileSchema>
    {
    }
}
using Deploy.LaunchPad.Core.Files.Formats;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface IPdfFile : IFile<byte[], PdfSchemaFormat>
    {
    }
}
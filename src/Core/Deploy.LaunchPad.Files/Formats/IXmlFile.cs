using System.Xml;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IXmlFile : IFile<XmlDocument, XmlSchemaSet>
    {
    }
}
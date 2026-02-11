using System.Xml;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Util.Files
{
    public partial interface IXmlFile : IFile<XmlDocument, XmlSchemaSet>
    {
    }
}
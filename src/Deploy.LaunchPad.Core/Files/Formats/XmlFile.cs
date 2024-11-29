using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Core.Files
{

    public partial class XmlFile : FileBase<XmlDocument>, IXmlFile
    {
        public override string Extension => ".xml";
    }
}

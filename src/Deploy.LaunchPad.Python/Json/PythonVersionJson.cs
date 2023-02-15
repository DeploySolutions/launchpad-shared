using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Python.Json
{
    
    [Serializable()]
    public partial class PythonVersionJson
    {
        public virtual int Major { get; private set; }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual int Minor { get; private set; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual int Patch { get; set; } = 0;
        public PythonVersionJson()
        {

        }

        public PythonVersionJson(int majorVersion, int minorVersion, int patchVersion)
        {
            Major = majorVersion;
            Minor = minorVersion;
            Patch = patchVersion;
        }
    }
}
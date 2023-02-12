using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Python
{
    
    public partial interface IPythonVersion:
        ISerializable,
        IComparable<PythonVersion>,
        IEquatable<PythonVersion>
    {
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        PythonMajorVersion MajorVersion { get;  }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        PythonMinorVersion MinorVersion { get;  }

        [DataObjectField(false)]
        [XmlElement]
        int PatchVersion { get; }

        [DataObjectField(false)]
        [XmlElement]
        DateTime FirstReleased { get; }

        [DataObjectField(false)]
        [XmlElement]
        DateTime EndOfSupport { get; }

        [DataObjectField(false)]
        [XmlElement]
        PythonMaintenanceStatus MaintenanceStatus { get; }

        [DataObjectField(false)]
        [XmlElement]
        Uri ReleaseNotes { get;  }


        [DataObjectField(false)]
        [XmlElement]
        Uri Download { get; }

        /// <summary>
        /// Returns the major, minor, and patch version number
        /// </summary>
        /// <returns></returns>
        public string GetFullVersion();

        /// <summary>
        /// Returns the major and minor version number only
        /// </summary>
        /// <returns></returns>
        public string GetShortVersion();
    }
}

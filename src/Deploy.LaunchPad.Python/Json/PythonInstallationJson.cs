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
    public partial class PythonInstallationJson
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string DescriptionShort { get; set; }

        public virtual string? DescriptionFull { get; set; }
        
        public virtual PythonVersionJson Version { get; set; }

        public virtual Uri InstallLocation { get; set; }

        public virtual IDictionary<string, Uri> ModuleLocations { get; set; }

        public PythonInstallationJson() : base()
        {
            Version = new PythonVersionJson(3, 11, 2);
            var comparer = StringComparer.OrdinalIgnoreCase;
            ModuleLocations = new Dictionary<string, Uri>(comparer);
            Id = "py" + Version.ToString();
            Name = "Python " + Version.ToString();
            DescriptionShort = Name;
            DescriptionFull = DescriptionShort;
        }
    }

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
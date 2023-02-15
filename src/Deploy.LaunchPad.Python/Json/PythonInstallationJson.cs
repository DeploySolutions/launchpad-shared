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
            
        }
    }

}
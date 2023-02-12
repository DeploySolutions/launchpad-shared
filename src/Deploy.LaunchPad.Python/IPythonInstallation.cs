using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Python
{
    public interface IPythonInstallation : ILaunchPadObject, ISerializable, IComparable<PythonInstallation>, IEquatable<PythonInstallation>
    {
        [DataObjectField(true)]
        [XmlAttribute]
        string Id { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        string Name { get; set; }

        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        string DescriptionShort { get; set; }

        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        string? DescriptionFull { get; set; }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        Uri InstallLocation { get; set; }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        IPythonVersion Version { get; set; }

        [DataObjectField(false)]
        [XmlElement]
        IDictionary<string, Uri> ModuleLocations { get; set; }
    }
}
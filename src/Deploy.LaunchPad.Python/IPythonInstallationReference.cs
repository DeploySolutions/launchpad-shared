using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Python
{
    public partial interface IPythonInstallationReference : ILaunchPadObject, ISerializable, IComparable<PythonInstallationReference>, IEquatable<PythonInstallationReference>
    {
        /// <summary>
        /// The ID of the Python Installation this reference points to
        /// </summary>
        [Required]
        [DataObjectField(true)]
        [XmlAttribute]
        public string InstallationId { get; set; }

        public IPythonInstallation GetPythonInstallationFromId(string id);

    }
}

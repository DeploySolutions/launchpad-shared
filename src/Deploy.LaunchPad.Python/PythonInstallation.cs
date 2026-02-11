// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-14-2023
// ***********************************************************************
// <copyright file="PythonInstallation.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Domain;
using Deploy.LaunchPad.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Class PythonInstallation.
    /// Implements the <see cref="Deploy.LaunchPad.Python.IPythonInstallation" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Python.IPythonInstallation" />
    [Serializable()]
    public partial class PythonInstallation : IPythonInstallation
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description short.
        /// </summary>
        /// <value>The description short.</value>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionShort { get; set; }

        /// <summary>
        /// Gets or sets the description full.
        /// </summary>
        /// <value>The description full.</value>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public virtual string? DescriptionFull { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual IPythonVersion Version { get; set; }

        /// <summary>
        /// Gets or sets the install location.
        /// </summary>
        /// <value>The install location.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual Uri InstallLocation { get; set; }

        /// <summary>
        /// Gets or sets the module locations.
        /// </summary>
        /// <value>The module locations.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual IDictionary<string, Uri> ModuleLocations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallation"/> class.
        /// </summary>
        protected PythonInstallation()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            ModuleLocations = new Dictionary<string, Uri>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallation"/> class.
        /// </summary>
        /// <param name="installLocation">The install location.</param>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="patchVersion">The patch version.</param>
        public PythonInstallation(Uri installLocation, PythonMajorVersion majorVersion, PythonMinorVersion minorVersion, int patchVersion)
        {
            string versionNumber = majorVersion.ToString() + "." + minorVersion.ToString() + "." + patchVersion.ToString();
            Version = new PythonReleaseRegistry().Releases[versionNumber];
            InstallLocation = installLocation;
            var comparer = StringComparer.OrdinalIgnoreCase;
            ModuleLocations = new Dictionary<string, Uri>(comparer);
            Id = "py" + (int)majorVersion + "." + (int)minorVersion + "." + patchVersion;
            Name = "Python " + (int)majorVersion + "." + (int)minorVersion + "." + patchVersion;
            DescriptionShort = Name;
            DescriptionFull = DescriptionShort;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallation"/> class.
        /// </summary>
        /// <param name="installLocation">The install location.</param>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="patchVersion">The patch version.</param>
        /// <param name="moduleLocations">The module locations.</param>
        public PythonInstallation(Uri installLocation, PythonMajorVersion majorVersion, PythonMinorVersion minorVersion, int patchVersion, IDictionary<string, Uri> moduleLocations)
        {
            string versionNumber = majorVersion.ToString() + "." + minorVersion.ToString() + "." + patchVersion.ToString();
            Version = new PythonReleaseRegistry().Releases[versionNumber];
            InstallLocation = installLocation;
            ModuleLocations = moduleLocations;
            Id = "py" + (int)majorVersion + "." + (int)minorVersion + "." + patchVersion;
            Name = "Python " + (int)majorVersion + "." + (int)minorVersion + "." + patchVersion;
            DescriptionShort = Name;
            DescriptionFull = DescriptionShort;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallation"/> class.
        /// </summary>
        /// <param name="installLocation">The install location.</param>
        /// <param name="moduleLocations">The module locations.</param>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="patchVersion">The patch version.</param>
        /// <param name="minorVersion">The minor version.</param>
        public PythonInstallation(Uri installLocation, IDictionary<string, Uri> moduleLocations, PythonMajorVersion majorVersion, int patchVersion, PythonMinorVersion minorVersion)
        {
            string versionNumber = majorVersion.ToString() + "." + minorVersion.ToString() + "." + patchVersion.ToString();
            Version = new PythonReleaseRegistry().Releases[versionNumber];
            InstallLocation = installLocation;
            ModuleLocations = moduleLocations;
            Id = "py" + (int)majorVersion + "." + (int)minorVersion + "." + patchVersion;
            Name = "Python " + (int)majorVersion + "." + (int)minorVersion + "." + patchVersion;
            DescriptionShort = Name;
            DescriptionFull = DescriptionShort;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected PythonInstallation(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetString("Id");
            Name = info.GetString("Name");
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
            InstallLocation = (Uri)info.GetValue("InstallLocation", typeof(Uri));
            Version = (PythonVersion)info.GetValue("Version", typeof(PythonVersion));
            ModuleLocations = (IDictionary<string, Uri>)info.GetValue("ModuleLocations", typeof(IDictionary<string, Uri>));
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Name", Name);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("InstallLocation", InstallLocation);
            info.AddValue("ModuleLocations", ModuleLocations);
            info.AddValue("Version", Version);
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
        public virtual int CompareTo(PythonInstallation other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[PythonInstallation: ");
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("InstallLocation={0};", InstallLocation);
            sb.AppendFormat("ModuleLocations={0};", ModuleLocations);
            sb.AppendFormat("Version={0};", Version);
            sb.Append(']');
            return sb.ToString();
        }


        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is PythonInstallation)
            {
                return Equals(obj as PythonInstallation);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Equals(PythonInstallation obj)
        {
            if (obj != null)
            {

                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Id.Equals(obj.Id) && Name.Equals(obj.Name) && Version.Equals(obj.Version)
                    && InstallLocation.Equals(obj.InstallLocation);

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(PythonInstallation x, PythonInstallation y)
        {
            if (x is null)
            {
                if (y is null)
                {
                    return true;
                }
                return false;
            }
            return x.Equals(y);
        }

        /// <summary>
        /// Override the != operator to test for inequality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are not equal based on the Equals logic</returns>
        public static bool operator !=(PythonInstallation x, PythonInstallation y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="Object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Id.GetHashCode()
                + Name.GetHashCode()
                + InstallLocation.GetHashCode()
                + Version.GetHashCode()
            ;
        }
    }
}

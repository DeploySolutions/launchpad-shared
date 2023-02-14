using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Core.Domain;
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
    [Serializable()]
    public partial class PythonInstallation : IPythonInstallation
    {
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Id { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; set; }

        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionShort { get; set; }

        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public virtual string? DescriptionFull { get; set; }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual IPythonVersion Version { get; set; }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual Uri InstallLocation { get; set; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual IDictionary<string, Uri> ModuleLocations { get; set; }

        protected PythonInstallation()
        {
            Version = new PythonReleaseRegistry().Releases["3.11.2"];
            var comparer = StringComparer.OrdinalIgnoreCase;
            ModuleLocations = new Dictionary<string, Uri>(comparer);
            Id = "py3.11.2";
            Name = "Python 3.11.2";
            DescriptionShort = Name;
            DescriptionFull = DescriptionShort;
        }

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
            Id = info.GetString(Id);
            Name = info.GetString(Name);
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
            InstallLocation = (Uri)info.GetValue("InstallLocation", typeof(Uri));
            Version = (PythonVersion)info.GetValue("Version", typeof(PythonVersion));
            ModuleLocations = (IDictionary<string, Uri>)info.GetValue("ModuleLocations", typeof(IDictionary<string, Uri>));
        }

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
        /// <returns></returns>
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
        public override String ToString()
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
        /// <returns></returns>
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
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
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

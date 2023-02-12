using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Deploy.LaunchPad.Python
{
    [Serializable]
    public partial class PythonVersion : IPythonVersion
    {

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual PythonMajorVersion MajorVersion { get; private set; }

        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual PythonMinorVersion MinorVersion { get; private set; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual int PatchVersion { get; set; } = 0;

        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime FirstReleased { get; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime EndOfSupport { get; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual PythonMaintenanceStatus MaintenanceStatus { get; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual Uri ReleaseNotes { get; }


        [DataObjectField(false)]
        [XmlElement]
        public virtual Uri Download { get; }

        /// <summary>
        /// Returns the major and minor version number only
        /// </summary>
        /// <returns></returns>
        public string GetShortVersion()
        {
            return MajorVersion.ToString() + "." + MinorVersion.ToString();
        }

        /// <summary>
        /// Returns major, minor, and patch version
        /// </summary>
        /// <returns></returns>
        public string GetFullVersion()
        {
            return MajorVersion.ToString() + "." + MinorVersion.ToString() + "." + PatchVersion;
        }

        public PythonVersion()
        {
            // default to the (hopefully current) latest version of Python available at https://www.python.org
            MajorVersion =   PythonMajorVersion.Three;
            MinorVersion=   PythonMinorVersion.Eleven;
            PatchVersion = 2;
            MaintenanceStatus = PythonMaintenanceStatus.BugFix;
            Download = new Uri("https://www.python.org/downloads/release/python-3112/");
            ReleaseNotes = new Uri("https://docs.python.org/release/3.11.2/whatsnew/changelog.html#python-3-11-2");
            FirstReleased = new DateTime(2022, 10, 24);
            EndOfSupport = new DateTime(2027, 10, 1);
        }

        public PythonVersion(PythonMajorVersion majorVersion, PythonMinorVersion minorVersion, int patchVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            PatchVersion = patchVersion;
        }

        public PythonVersion(
            PythonMajorVersion majorVersion,
            PythonMinorVersion minorVersion,
            int patchVersion,
            DateTime firstReleased,
            DateTime endOfSupport,
            PythonMaintenanceStatus maintenanceStatus,
            Uri download,
            Uri releaseNotes
        )
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            PatchVersion = patchVersion; 
            FirstReleased = firstReleased;
            EndOfSupport = endOfSupport;
            MaintenanceStatus = maintenanceStatus;
            Download = download;
            ReleaseNotes = releaseNotes;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected PythonVersion(SerializationInfo info, StreamingContext context)
        {
            MajorVersion = (PythonMajorVersion)info.GetValue("MajorVersion", typeof(PythonMajorVersion));
            MinorVersion = (PythonMinorVersion)info.GetValue("MinorVersion", typeof(PythonMinorVersion));
            PatchVersion = info.GetInt32("PatchVersion");
            FirstReleased = info.GetDateTime("FirstReleased");
            EndOfSupport = info.GetDateTime("EndOfSupport");
            MaintenanceStatus = (PythonMaintenanceStatus)info.GetValue("MaintenanceStatus", typeof(PythonMaintenanceStatus));
            Download = (Uri)info.GetValue("Download", typeof(Uri));
            ReleaseNotes = (Uri)info.GetValue("ReleaseNotes", typeof(Uri));          
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MajorVersion", MajorVersion);
            info.AddValue("MinorVersion", MinorVersion);
            info.AddValue("PatchVersion", PatchVersion);
            info.AddValue("FirstReleased", FirstReleased);
            info.AddValue("EndOfSupport", EndOfSupport);
            info.AddValue("MaintenanceStatus", MaintenanceStatus);
            info.AddValue("Download", Download);
            info.AddValue("ReleaseNotes", ReleaseNotes);
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(PythonVersion other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return MajorVersion.CompareTo(other.MajorVersion) +
                MinorVersion.CompareTo(other.MinorVersion) +
                PatchVersion.CompareTo(other.PatchVersion)
            ;
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[PythonVersion: ");
            sb.AppendFormat("MajorVersion={0};", MajorVersion);
            sb.AppendFormat("MinorVersion={0};", MinorVersion);
            sb.AppendFormat("PatchVersion={0};", PatchVersion);
            sb.AppendFormat("FirstReleased={0};", FirstReleased);
            sb.AppendFormat("EndOfSupport={0};", EndOfSupport);
            sb.AppendFormat("MaintenanceStatus={0};", MaintenanceStatus);
            sb.AppendFormat("Download={0};", Download);
            sb.AppendFormat("ReleaseNotes={0};", ReleaseNotes);
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
            if (obj != null && obj is PythonVersion)
            {
                return Equals(obj as PythonVersion);
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
        public virtual bool Equals(PythonVersion obj)
        {
            if (obj != null)
            {

                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return 
                    MajorVersion.Equals(obj.MajorVersion) &&
                    MinorVersion.Equals(obj.MinorVersion) &&
                    PatchVersion.Equals(obj.PatchVersion) &&
                    FirstReleased.Equals(obj.FirstReleased) &&
                    EndOfSupport.Equals(obj.EndOfSupport) &&
                    MaintenanceStatus.Equals(obj.MaintenanceStatus) &&
                    Download.Equals(obj.Download) &&
                    ReleaseNotes.Equals(obj.ReleaseNotes)
                ;

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(PythonVersion x, PythonVersion y)
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
        public static bool operator !=(PythonVersion x, PythonVersion y)
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
            return MajorVersion.GetHashCode()
                + MinorVersion.GetHashCode()
                + PatchVersion.GetHashCode()
            ;
        }
    }
}

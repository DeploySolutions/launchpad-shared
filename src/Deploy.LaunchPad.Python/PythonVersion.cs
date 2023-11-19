// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-11-2023
// ***********************************************************************
// <copyright file="PythonVersion.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class PythonVersion.
    /// Implements the <see cref="Deploy.LaunchPad.Python.IPythonVersion" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Python.IPythonVersion" />
    [Serializable]
    public partial class PythonVersion : IPythonVersion
    {

        /// <summary>
        /// Gets the major version.
        /// </summary>
        /// <value>The major version.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual PythonMajorVersion MajorVersion { get; private set; }

        /// <summary>
        /// Gets the minor version.
        /// </summary>
        /// <value>The minor version.</value>
        [Required]
        [DataObjectField(false)]
        [XmlElement]
        public virtual PythonMinorVersion MinorVersion { get; private set; }

        /// <summary>
        /// Gets the patch version.
        /// </summary>
        /// <value>The patch version.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual int PatchVersion { get; set; } = 0;

        /// <summary>
        /// Gets the first released.
        /// </summary>
        /// <value>The first released.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime FirstReleased { get; }

        /// <summary>
        /// Gets the end of support.
        /// </summary>
        /// <value>The end of support.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime EndOfSupport { get; }

        /// <summary>
        /// Gets the maintenance status.
        /// </summary>
        /// <value>The maintenance status.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual PythonMaintenanceStatus MaintenanceStatus { get; }

        /// <summary>
        /// Gets the release notes.
        /// </summary>
        /// <value>The release notes.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual Uri ReleaseNotes { get; }


        /// <summary>
        /// Gets the download.
        /// </summary>
        /// <value>The download.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual Uri Download { get; }

        /// <summary>
        /// Returns the major and minor version number only
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetShortVersion()
        {
            return MajorVersion.ToString() + "." + MinorVersion.ToString();
        }

        /// <summary>
        /// Returns major, minor, and patch version
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetFullVersion()
        {
            return MajorVersion.ToString() + "." + MinorVersion.ToString() + "." + PatchVersion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonVersion"/> class.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonVersion"/> class.
        /// </summary>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="patchVersion">The patch version.</param>
        public PythonVersion(PythonMajorVersion majorVersion, PythonMinorVersion minorVersion, int patchVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            PatchVersion = patchVersion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonVersion"/> class.
        /// </summary>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="patchVersion">The patch version.</param>
        /// <param name="firstReleased">The first released.</param>
        /// <param name="endOfSupport">The end of support.</param>
        /// <param name="maintenanceStatus">The maintenance status.</param>
        /// <param name="download">The download.</param>
        /// <param name="releaseNotes">The release notes.</param>
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

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
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
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
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
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="Object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return MajorVersion.GetHashCode()
                + MinorVersion.GetHashCode()
                + PatchVersion.GetHashCode()
            ;
        }
    }
}

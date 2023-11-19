// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-14-2023
// ***********************************************************************
// <copyright file="PythonInstallationReference.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Class PythonInstallationReference.
    /// Implements the <see cref="Deploy.LaunchPad.Python.IPythonInstallationReference" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Python.IPythonInstallationReference" />
    [Serializable()]
    public partial class PythonInstallationReference : IPythonInstallationReference
    {
        /// <summary>
        /// The ID of the Python Installation this reference points to
        /// </summary>
        /// <value>The installation identifier.</value>
        public virtual string InstallationId { get;set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallationReference"/> class.
        /// </summary>
        public PythonInstallationReference()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonInstallationReference"/> class.
        /// </summary>
        /// <param name="installationId">The installation identifier.</param>
        public PythonInstallationReference(string installationId)
        {
            InstallationId=installationId;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected PythonInstallationReference(SerializationInfo info, StreamingContext context)
        {
            InstallationId = info.GetString("InstallationId");        
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InstallationId", InstallationId);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
        public int CompareTo(PythonInstallationReference other)
        {
            return InstallationId.CompareTo(other.InstallationId);
        }

        /// <summary>
        /// Gets the python installation from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IPythonInstallation.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IPythonInstallation GetPythonInstallationFromId(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[PythonInstallationReference: ");
            sb.AppendFormat("InstallationId={0};", InstallationId);
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
            if (obj != null && obj is PythonInstallationReference)
            {
                return Equals(obj as PythonInstallationReference);
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
        public virtual bool Equals(PythonInstallationReference obj)
        {
            if (obj != null)
            {

                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return InstallationId.Equals(obj.InstallationId);

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(PythonInstallationReference x, PythonInstallationReference y)
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
        public static bool operator !=(PythonInstallationReference x, PythonInstallationReference y)
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
            return InstallationId.GetHashCode();
        }
    }
}

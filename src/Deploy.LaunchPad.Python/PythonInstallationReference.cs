using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Deploy.LaunchPad.Python
{
    [Serializable()]
    public partial class PythonInstallationReference : IPythonInstallationReference
    {
        public virtual string InstallationId { get;set; } = string.Empty;

        public PythonInstallationReference()
        {

        }

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
            InstallationId = info.GetString(InstallationId);            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InstallationId", InstallationId);
        }

        public int CompareTo(PythonInstallationReference other)
        {
            return InstallationId.CompareTo(other.InstallationId);
        }

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
        /// <returns></returns>
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
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return InstallationId.GetHashCode();
        }
    }
}

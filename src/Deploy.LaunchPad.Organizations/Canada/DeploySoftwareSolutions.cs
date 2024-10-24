// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Organizations
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="DeploySoftwareSolutions.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

using Deploy.LaunchPad.Core.Abp.Domain;
using Schema.NET;
using System;
using System.Text;

namespace Deploy.LaunchPad.Organizations.Canada
{


    /// <summary>
    /// Class DeploySolutions.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Domain.LaunchPadOrganizationBase{System.Guid}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.LaunchPadOrganizationBase{System.Guid}" />
    public partial class DeploySolutions : LaunchPadOrganizationBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploySolutions"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public DeploySolutions(int? tenantId) : base(tenantId)
        {
            Organization org = new Organization()
            {
                Name = "Deploy Software Solutions, inc.",
                LegalName = "Deploy Software Solutions, inc.",

                Address = new PostalAddress()
                {
                    AddressLocality = "Ottawa",
                    AddressRegion = "Ontario",
                    AddressCountry = "Canada",
                },
                Url = new Uri("https://www.deploy.solutions"),
                Email = "support@deploy.solutions"

            };
            _schema = org;



        }


        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(DeploySolutions other)
        {
            if (other == null) return 1;
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[LaunchPadOrganizationBase: ");
            //  sb.Append(base.ToStringBaseProperties());
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
            if (obj != null && obj is LaunchPadOrganizationBase)
            {
                return Equals(obj as LaunchPadOrganizationBase);
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(DeploySolutions x, DeploySolutions y)
        {
            if (System.Object.ReferenceEquals(x, null))
            {
                if (System.Object.ReferenceEquals(y, null))
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
        public static bool operator !=(DeploySolutions x, DeploySolutions y)
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
            return Id.GetHashCode();
        }


    }
}

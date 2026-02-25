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

using Deploy.LaunchPad.Core.Abp;
using Deploy.LaunchPad.Core.Abp.Organization;
using Schema.NET;
using System;

namespace Deploy.LaunchPad.Organizations.Canada
{


    /// <summary>
    /// Class DeploySolutions.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.OrganizationDomainEntityBase{System.Guid}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.OrganizationDomainEntityBase{System.Guid}" />
    public partial class DeploySolutions : OrganizationDomainEntityBase<Guid>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploySolutions"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public DeploySolutions(System.Guid? tenantId) : base(tenantId)
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
            _schemaDotOrg = org;



        }



    }
}

// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Organizations
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="CanadaSpaceAgency.cs" company="Deploy Software Solutions, inc.">
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
    /// Class CanadaSpaceAgency.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.GovernmentOrganizationBase{System.Guid}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.GovernmentOrganizationBase{System.Guid}" />
    public partial class CanadianSpaceAgency : GovernmentOrganizationBase<Guid>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadianSpaceAgency"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public CanadianSpaceAgency(System.Guid? tenantId) : base(tenantId)
        {
            GovernmentOrganization org = new GovernmentOrganization()
            {
                Name = "Canadian Space Agency/Agence spatiale canadienne",
                LegalName = "Canadian Space Agency/Agence spatiale canadienne",
                NumberOfEmployees = new QuantitativeValue()
                {
                    MinValue = 670,
                    MaxValue = 690
                },
                Address = new PostalAddress()
                {
                    AddressLocality = "St Hubert",
                    AddressRegion = "Quebec",
                    AddressCountry = "Canada",
                },
                Url = new Uri("https://www.asc-csa.gc.ca/"),
                Email = "asc.info.csa@canada.ca"

            };
            Schema = org;



        }



    }
}

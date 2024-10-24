﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Organizations
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="DefenceResearchDevelopmentCanada.cs" company="Deploy Software Solutions, inc.">
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
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Schema.NET;
using System;

namespace Deploy.LaunchPad.Organizations.Canada
{
    /// <summary>
    /// Class DefenceResearchDevelopmentCanada.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Domain.GovernmentOrganizationBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.GovernmentOrganizationBase" />
    public partial class DefenceResearchDevelopmentCanada : GovernmentOrganizationBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DefenceResearchDevelopmentCanada"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public DefenceResearchDevelopmentCanada(int? tenantId) : base(tenantId)
        {
            GovernmentOrganization org = new GovernmentOrganization()
            {
                Name = "Defence Research and Development Canada/Recherche et développement pour la défense Canada",
                LegalName = "Defence Research and Development Canada/Recherche et développement pour la défense Canada",
                NumberOfEmployees = new QuantitativeValue()
                {
                    MinValue = 1400,
                    MaxValue = 1450
                },
                Address = new PostalAddress()
                {
                    AddressLocality = "Ottawa",
                    AddressRegion = "Ontario",
                    AddressCountry = "Canada",
                    PostalCode = "K1A 0K2",
                    StreetAddress = "National Defence Headquarters Major - General George R.Pearkes Building 101 Colonel By Dr"
                },
                Url = new Uri("http://www.drdc-rddc.gc.ca/"),
                Email = "information@forces.gc.ca"

            };
            Schema = org;

        }

    }
}

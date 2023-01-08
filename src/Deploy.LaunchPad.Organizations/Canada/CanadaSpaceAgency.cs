//LaunchPad Space
// Copyright (c) 2018-2022 Deploy Software Solutions, inc. 

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

namespace Deploy.LaunchPad.Organizations.Canada
{
    public class CanadaSpaceAgency : GovernmentOrganizationBase<Guid>
    {

        public CanadaSpaceAgency(int? tenantId) : base(tenantId)
        {
            GovernmentOrganization org = new GovernmentOrganization()
            {
                Name = "Canada Space Agency/Agence spatiale canadienne",
                LegalName = "Canada Space Agency/Agence spatiale canadienne",
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
                Url = new Uri("http://www.asc-csa.gc.ca/"),
                Email = "asc.info.csa@canada.ca"

            };
            Schema = org;



        }



    }
}

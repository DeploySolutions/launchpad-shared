//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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


namespace DeploySoftware.LaunchPad.Shared.Domain
{
    
    using System;

    public class UsageRights : IUsageRights
    {
        public string Owner { get; set; }

        public string Attribution { get; set; }

        public ILicense GoverningLicense { get; set; }

        public Uri ProjectLink { get; set; }

        public UsageRights()
        {}

    public UsageRights(string owner, string attribution, ILicense license)
        {
            Owner = owner;
            Attribution = attribution;
            GoverningLicense = license;
        }

        public UsageRights(string owner, string attribution, ILicense license, Uri projectLink)
        {
            Owner = owner;
            Attribution = attribution;
            GoverningLicense = license;
            ProjectLink = projectLink;
        }

    }
}

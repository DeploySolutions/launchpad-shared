﻿//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Core.Domain
{
    
    using System;

    /// <summary>
    /// Identifies a license to assist with identifying and remaining compliant with its terms
    /// </summary>
    public abstract class License : ILicense
    {
        /// <summary>
        /// The name of the license
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// A brief human-readable description of the license
        /// </summary>
        public abstract string Summary { get; }    
        
        /// <summary>
        /// A link to the full terms of the license
        /// </summary>
        public abstract Uri FullTermsUrl { get; }
        
    }
}

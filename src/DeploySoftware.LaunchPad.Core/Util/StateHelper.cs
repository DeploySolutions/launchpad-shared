//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 
//This file is a derivative work from the original created in NCommon and copyright 2010 by Ritesh Rao 

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

namespace DeploySoftware.LaunchPad.Core.Util
{
    using DeploySoftware.LaunchPad.Core.Domain;

    ///<summary>
    /// Utility class for NCommon.State.
    ///</summary>
    public static class StateHelper
    {
        ///<summary>
        /// Builds a key from the full name of the type and the supplied user key.
        ///</summary>
        ///<param name="userKey">The user supplied key, if any.</param>
        ///<typeparam name="T">The type for which the key is built.</typeparam>
        ///<returns>string.</returns>
        public static string BuildFullKey<T>(this object userKey)
        {
            if (userKey == null)
                return typeof(T).FullName;
            return typeof(T).FullName + userKey;
        }
    }
}
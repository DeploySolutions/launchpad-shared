//LaunchPad Shared
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Shared.Util
{
    public static class StringParser
    {
        /// <summary>
        /// Parse a string and return a value between two anchor strings.
        /// Adapted work from ChaseMedallion post on Stack Overflow: https://stackoverflow.com/questions/17252615/get-string-between-two-strings-in-a-string
        /// Licensed by Stack Overflow under Creative Commons Attribution-ShareAlike 4.0 International Public License https://creativecommons.org/licenses/by-sa/4.0/
        /// </summary>
        /// <param name="@this">The string to search within</param>
        /// <param name="searchAfter">Start searching immediately after the first incidence of this anchor string</param>
        /// <param name="searchBefore">End searching immediately before the first incidence of this anchor string</param>
        /// <param name="trimWhitespaceBefore">Optionally trim all whitespace at the start of the search result</param>
        /// <param name="trimWhitespaceAfter">Optionally trim all whitespace at the end of the search result</param>
        /// <returns>A string value resulting from the search, or String.Empty if nothing is found</returns>
        public static String FindStringWithinAnchorText(this String @this, String searchAfter, String searchBefore, Boolean trimWhitespaceBefore = false, Boolean trimWhitespaceAfter = false, StringComparison cultureComparison = StringComparison.InvariantCulture)
        {
            String searchResult = String.Empty;
            var fromLength = (searchAfter ?? String.Empty).Length;
            var startAt = !String.IsNullOrEmpty(searchAfter)
                ? @this.IndexOf(searchAfter, cultureComparison) + fromLength
                : 0;
            var endAt = !String.IsNullOrEmpty(searchBefore)
                ? @this.IndexOf(searchBefore, startAt, cultureComparison)
                : @this.Length;
            if (endAt >= 0)
            {
                searchResult = @this.Substring(startAt, endAt - startAt);
                if (trimWhitespaceBefore)
                {
                    searchResult = searchResult.TrimStart();
                }
                if (trimWhitespaceAfter)
                {
                    searchResult = searchResult.TrimEnd();
                }
            }
            return searchResult;
        }
    }
}

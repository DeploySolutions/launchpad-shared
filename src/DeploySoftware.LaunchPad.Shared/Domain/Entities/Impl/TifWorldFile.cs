//LaunchPad Space
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
    // Tiff World File (TFW)
    //
    public class TifWorldFile<TPrimaryKey> : FileBase<TPrimaryKey>
    {
        public override string FileExtension => ".tfw";

        /// <summary>
        /// Line 1: A: x-scale
        /// </summary>
        public decimal A { get; set; }

        /// <summary>
        /// Line 2: D: y-skew
        /// </summary>
        public decimal D { get; set; }

        /// <summary>
        /// Line 3: B: x-skew
        /// </summary>
        public decimal B { get; set; }

        /// <summary>
        /// Line 4: E: y-scale
        /// </summary>
        public decimal E { get; set; }

        /// <summary>
        /// Line 5: UTM easting of centre of upper-left pixel
        /// </summary>
        public decimal C { get; set; }

        /// <summary>
        /// Line 5: UTM northing of centre of upper-left pixel
        /// </summary>
        public decimal F { get; set; }

    }
}

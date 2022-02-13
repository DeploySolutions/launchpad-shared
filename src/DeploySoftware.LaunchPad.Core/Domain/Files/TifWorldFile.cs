//LaunchPad Space
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

using Schema.NET;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    // Tiff World File (TFW)
    //
    public class TifWorldFile<TPrimaryKey, TFileStorageLocationType> : FileBase<TPrimaryKey, byte[],TFileStorageLocationType>
        where TFileStorageLocationType: IFileStorageLocation, new()
    {
        public override string Extension { get => ".tfw"; }

        /// <summary>
        /// Line 1: A: x-scale
        /// </summary>
        public virtual decimal A { get; set; }

        /// <summary>
        /// Line 2: D: y-skew
        /// </summary>
        public virtual decimal D { get; set; }

        /// <summary>
        /// Line 3: B: x-skew
        /// </summary>
        public virtual decimal B { get; set; }

        /// <summary>
        /// Line 4: E: y-scale
        /// </summary>
        public virtual decimal E { get; set; }

        /// <summary>
        /// Line 5: UTM easting of centre of upper-left pixel
        /// </summary>
        public virtual decimal C { get; set; }

        /// <summary>
        /// Line 5: UTM northing of centre of upper-left pixel
        /// </summary>
        public virtual decimal F { get; set; }

    }
}

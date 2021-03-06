﻿//LaunchPad Space
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
    public class TifFile<TIdType, TFileStorageLocationType> : FileBase<TIdType, TFileStorageLocationType>
        where TFileStorageLocationType: IFileStorageLocation, new()
    {
        public override string Extension => ".tif";

        public TifFile() : base()
        {

        }
        public TifFile(string fileName) : base(fileName)
        {

        }

        public TifFile(TIdType id, string fileName) : base(id, fileName)
        {

        }
    }
}

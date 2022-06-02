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

using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    [Owned]
    public partial class WindowsFileStorageLocation : FileStorageLocationBase
    {


        public WindowsFileStorageLocation() :base()
        {
            string defaultUri = string.Format("file:///{0}", Directory.GetCurrentDirectory());
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", defaultUri);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            RootUri = new Uri(defaultUri);
            Provider = FileStorageProviderTypeEnum.Windows_NTFS;
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected WindowsFileStorageLocation(SerializationInfo info, StreamingContext context) :base (info,context)
        {

        }

    }
}

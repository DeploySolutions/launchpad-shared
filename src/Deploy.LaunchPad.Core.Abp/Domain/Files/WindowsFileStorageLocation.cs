//LaunchPad Shared
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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

using Deploy.LaunchPad.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    [Owned]
    public partial class WindowsFileStorageLocation : GenericFileStorageLocation
    {


        public WindowsFileStorageLocation() : base()
        {
            string defaultUri = string.Format("file:///{0}", Directory.GetCurrentDirectory());
            FileInfo file = new FileInfo(defaultUri);
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);
            string driveRoot = drive.RootDirectory.FullName;
            string descriptionMessage = string.Format("Windows file share drive '{0}'", driveRoot);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            RootUri = new Uri(defaultUri);
            Provider = FileStorageLocationTypeEnum.Windows_NTFS;
        }

        public WindowsFileStorageLocation(string id, Uri rootUri) : base(id, rootUri)
        {
            FileInfo file = new FileInfo(RootUri.ToString());
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);
            string driveRoot = drive.RootDirectory.FullName;
            string descriptionMessage = string.Format("Windows file share drive '{0}'", driveRoot);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            Provider = FileStorageLocationTypeEnum.Windows_NTFS;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected WindowsFileStorageLocation(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[WindowsFileStorageLocation: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }


        /// <summary>
        /// Returns available storage space for this location, in bytes, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns></returns>
        public override long GetAvailableStorageSpaceInBytes()
        {
            DriveInfo drive = new DriveInfo(RootUri.LocalPath);
            if (drive.IsReady)
            {
                return drive.AvailableFreeSpace;
            }
            return -1;
        }

        /// <summary>
        /// Returns available storage space for this location, in GB, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns></returns>
        public override long GetAvailableStorageSpaceInGigabytes()
        {
            long driveSpace = GetAvailableStorageSpaceInBytes();
            if (driveSpace > 0)
            {
                return driveSpace / (1024 * 1024 * 1024);
            }
            return -1;
        }


    }
}

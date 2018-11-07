//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 
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

namespace DeploySoftware.LaunchPad.Shared.Repositories
{
    using DeploySoftware.LaunchPad.Shared.Domain;

    ///<summary>
    /// A base class for implementors of <see cref="IRepository{TLaunchPadObject}"/>.
    /// This repository type only assumes it can read, but not perform other more advanced functionality such as attaching/detaching, refreshing, or adding/deleting.    
    ///</summary>
    ///<typeparam name="TLaunchPadObject"></typeparam>
    public abstract class WriteableRepositoryBase<TLaunchPadObject> : ReadableRepositoryBase<TLaunchPadObject>, IWriteableRepository<TLaunchPadObject>
         where TLaunchPadObject : ILaunchPadObject, new()
    {
        
        /// <summary>
        /// A <see cref="WriteableRepositoryMethodsBase{TLaunchPadObject}">WriteableRepositoryMethodsBase{TLaunchPadObject}</see> containing of
        /// methods used to modify a repository's collection of data
        /// </summary>
        public abstract WriteableRepositoryMethodsBase<TLaunchPadObject> Write
        { get; set; }        
        
    }
}

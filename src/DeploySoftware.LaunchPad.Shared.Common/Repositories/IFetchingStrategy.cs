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
    using System.Linq;

    ///<summary>
    /// Specifies a fetching strategy for a <see cref="IRepository{TEntity}"/> instance.
    ///</summary>
    public interface IFetchingStrategy<TEntity, TForService>
         where TEntity : ILaunchPadObject, new()
    {
        ///<summary>
        /// Instructs the instance to define the fetching strategy on the repository instance.
        ///</summary>
        ///<param name="repository"></param>
        IQueryable<TEntity> Define(IRepository<TEntity> repository);
    }
}
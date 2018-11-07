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

using System.Linq;

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    /// <summary>
    /// Defines the transaction mode when creating a new <see cref="UnitOfWorkScope"/> instance.
    /// </summary>
    public enum TransactionMode
    {
        /// <summary>
        /// Specifies that the <see cref="UnitOfWorkScope"/> should be created using default
        /// transaction mode.
        /// </summary>
        /// <remarks>
        /// The default transaction mode instructs the <see cref="UnitOfWorkScope"/> to enlist in
        /// a parent <see cref="UnitOfWorkScope"/>'s transaction, or if one doesnt exist, then
        /// creates a new transaction.
        /// </remarks>
        Default = 0,
        /// <summary>
        /// Specifies that the scope should not participate in a parent <see cref="UnitOfWorkScope"/>'s transaction,
        /// if one exists, and should start it's own transaction.
        /// </summary>
        New = 1,
        /// <summary>
        /// Specifies that the <see cref="UnitOfWorkScope"/> should not participate in a parent scope's transaction,
        /// and should not start a transaction of its own.
        /// </summary>
        /// <remarks>
        /// If a scope is created using the Supress option, any child scopes created with the default 
        /// transaction mode, i.e. <see cref="Default"/> will also not participate in any transaction, although
        /// it will share the same parent <see cref="IUnitOfWork"/> instance.
        /// </remarks>
        Supress = 2
    }
}
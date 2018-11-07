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

using Common.Logging;
using System;
using System.Linq;

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    ///<summary>
    /// Gets an instances of <see cref="ITransactionManager"/>.
    ///</summary>
    public static class UnitOfWorkManager
    {
        static Func<ITransactionManager> _provider;
        static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWorkManager));
        private const string LocalTransactionManagerKey = "UnitOfWorkManager.LocalTransactionManager";
        static readonly Func<ITransactionManager> DefaultTransactionManager = () =>
        {
            Logger.Debug(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Debug_UnitOfWorkManager_DefaultTransactionManager_UsingDefaultProvider));
            ITransactionManager transactionManager = null;
            if (transactionManager == null)
            {
                Logger.Debug(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Debug_UnitOfWorkManager_DefaultTransactionManager_CreatingNewTransactionManager));
                transactionManager = new TransactionManager();
                //TODO: Store the transaction manager somewhere
                // state.Local.Put(LocalTransactionManagerKey, transactionManager);
                 
            }
            return transactionManager;
        };

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="UnitOfWorkManager"/>.
        /// </summary>
        static UnitOfWorkManager()
        {
            _provider = DefaultTransactionManager;
        }

        ///<summary>
        /// Sets a <see cref="Func{T}"/> of <see cref="ITransactionManager"/> that the 
        /// <see cref="UnitOfWorkManager"/> uses to get an instance of <see cref="ITransactionManager"/>
        ///</summary>
        ///<param name="provider"></param>
        public static void SetTransactionManagerProvider(Func<ITransactionManager> provider)
        {
            if (provider == null)
            {
                Logger.Debug(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Debug_UnitOfWorkManager_SetTransactionManagerProvider_SetToNull));
                _provider = DefaultTransactionManager;
                return;
            }
            Logger.Debug(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Debug_UnitOfWorkManager_SetTransactionManagerProvider_ProviderBeingOverriden));
            _provider = provider;
        }

        /// <summary>
        /// Gets the current <see cref="ITransactionManager"/>.
        /// </summary>
        public static ITransactionManager CurrentTransactionManager
        {
            get
            {
                return _provider();
            }
        }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> instance.
        /// </summary>
        public static IUnitOfWork CurrentUnitOfWork
        {
            get
            {
                return _provider().CurrentUnitOfWork;
            }
        }
    }
}
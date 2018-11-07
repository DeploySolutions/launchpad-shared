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
using DeploySoftware.LaunchPad.Common.Repositories;
using DeploySoftware.LaunchPad.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    /// <summary>
    /// Default implementation of <see cref="ITransactionManager"/> interface.
    /// </summary>
    public class TransactionManager : ITransactionManager, IDisposable
    {
        bool _disposed;
        readonly Guid _transactionManagerId = Guid.NewGuid();
        readonly ILog _logger = LogManager.GetLogger<TransactionManager>();
        readonly LinkedList<UnitOfWorkTransaction> _transactions = new LinkedList<UnitOfWorkTransaction>();

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="TransactionManager"/> class.
        /// </summary>
        public TransactionManager()
        {
            _logger.Debug(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Debug_TransactionManager_TransactionManager, _transactionManagerId));
        }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> instance.
        /// </summary>
        public IUnitOfWork CurrentUnitOfWork
        {
            get 
            {
                return CurrentTransaction == null ? null : CurrentTransaction.UnitOfWork;
            }
        }

        /// <summary>
        /// Gets the current <see cref="UnitOfWorkTransaction"/> instance.
        /// </summary>
        public UnitOfWorkTransaction CurrentTransaction
        {
            get
            {
                return _transactions.Count == 0 ? null : _transactions.First.Value;
            }
        }

        /// <summary>
        /// Enlists a <see cref="UnitOfWorkScope"/> instance with the transaction manager,
        /// with the specified transaction mode.
        /// </summary>
        /// <param name="scope">The <see cref="IUnitOfWorkScope"/> to register.</param>
        /// <param name="mode">A <see cref="TransactionMode"/> enum specifying the transaciton
        /// mode of the unit of work.</param>
        public void EnlistScope(IUnitOfWorkScope scope, TransactionMode mode)
        {
            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_TransactionManager_EnlistScope_EnlistingScope,
                                scope.ScopeId,
                                _transactionManagerId,
                                mode));
            
            IUnitOfWorkFactory uowFactory = new InMemoryUnitOfWorkFactory(); // TODO: Create uowFactory via DI
            if (_transactions.Count == 0 || 
                mode == TransactionMode.New ||
                mode == TransactionMode.Supress)
            {
                _logger.Debug(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_TransactionManager_EnlistScope_EnlistingScopeRequiresNewTransactionScope, scope.ScopeId, mode));
                var txScope = TransactionScopeHelper.CreateScope(UnitOfWorkSettings.DefaultIsolation, mode);
                var unitOfWork = uowFactory.Create();
                var transaction = new UnitOfWorkTransaction(unitOfWork, txScope);
                transaction.TransactionDisposing += OnTransactionDisposing;
                transaction.EnlistScope(scope);
                _transactions.AddFirst(transaction);
                return;
            }
            CurrentTransaction.EnlistScope(scope);
        }

        /// <summary>
        /// Handles a Dispose signal from a transaction.
        /// </summary>
        /// <param name="transaction"></param>
        void OnTransactionDisposing(UnitOfWorkTransaction transaction)
        {
            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_TransactionManager_OnTransactionDisposing,
                                    transaction.TransactionId, _transactionManagerId));

            transaction.TransactionDisposing -= OnTransactionDisposing;
            var node = _transactions.Find(transaction);
            if (node != null)
                _transactions.Remove(node);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal dispose.
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_TransactionManager_Dispose, _transactionManagerId));
                if (_transactions != null && _transactions.Count > 0)
                {
                    _transactions.ForEach(tx =>
                    {
                        tx.TransactionDisposing -= OnTransactionDisposing;
                        tx.Dispose();
                    });
                    _transactions.Clear();
                }
            }
            _disposed = true;
        }
    }
}
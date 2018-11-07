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
using DeploySoftware.LaunchPad.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    /// <summary>
    /// Encapsulates a unit of work transaction.
    /// </summary>
    public class UnitOfWorkTransaction : IDisposable
    {
        bool _disposed;
        TransactionScope _transaction;
        IUnitOfWork _unitOfWork;
        IList<IUnitOfWorkScope> _attachedScopes = new List<IUnitOfWorkScope>();

        readonly Guid _transactionId = Guid.NewGuid();
        readonly ILog _logger = LogManager.GetLogger<UnitOfWorkTransaction>();
        

        ///<summary>
        /// Raised when the transaction is disposing.
        ///</summary>
        public event Action<UnitOfWorkTransaction> TransactionDisposing;

        ///<summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="UnitOfWorkTransaction"/> class.
        ///</summary>
        ///<param name="unitOfWork">The <see cref="IUnitOfWork"/> instance managed by the 
        /// <see cref="UnitOfWorkTransaction"/> instance.</param>
        ///<param name="transaction">The <see cref="TransactionScope"/> instance managed by 
        /// the <see cref="UnitOfWorkTransaction"/> instance.</param>
        public UnitOfWorkTransaction(IUnitOfWork unitOfWork, TransactionScope transaction)
        {
            Guard.Against<ArgumentNullException>(unitOfWork == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_UnitOfWorkTransaction_UnitOfWorkTransaction_ArgumentNullException_unitOfWork);
            Guard.Against<ArgumentNullException>(transaction == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_UnitOfWorkTransaction_UnitOfWorkTransaction_ArgumentNullException_unitOfWork);
            _unitOfWork = unitOfWork;
            _transaction = transaction;
            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_UnitOfWorkTransaction_Created, _transactionId));
        }

        ///<summary>
        /// Gets the unique transaction id of the <see cref="UnitOfWorkTransaction"/> instance.
        ///</summary>
        /// <value>A <see cref="Guid"/> representing the unique id of the <see cref="UnitOfWorkTransaction"/> instance.</value>
        public Guid TransactionId
        {
            get { return _transactionId; }
        }

        /// <summary>
        /// Gets the <see cref="IUnitOfWork"/> instance managed by the 
        /// <see cref="UnitOfWorkTransaction"/> instance.
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// Attaches a <see cref="UnitOfWorkScope"/> instance to the 
        /// <see cref="UnitOfWorkTransaction"/> instance.
        /// </summary>
        /// <param name="scope">The <see cref="UnitOfWorkScope"/> instance to attach.</param>
        public void EnlistScope(IUnitOfWorkScope scope)
        {
            Guard.Against<ArgumentNullException>(scope == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_UnitOfWorkTransaction_EnlistScope_ArgumentNullException);

            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_EnlistScope_Enlisted, scope.ScopeId, _transactionId));
            _attachedScopes.Add(scope);
            scope.ScopeComitting += OnScopeCommitting;
            scope.ScopeRollingback += OnScopeRollingBack;
        }

        /// <summary>
        /// Callback executed when an enlisted scope has comitted.
        /// </summary>
        void OnScopeCommitting(IUnitOfWorkScope scope)
        {
            Guard.Against<ObjectDisposedException>(_disposed, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_UnitOfWorkTransaction_OnScopeCommitting_ObjectDisposedException);

            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_OnScopeCommitting_Signalled, scope.ScopeId, _transactionId));
           if (!_attachedScopes.Contains(scope))
           {
               Dispose();
               throw new InvalidOperationException(DeploySoftware_LaunchPad_Shared_Common_Resources.Exception_UnitOfWorkTransaction_OnScopeCommitting_InvalidOperationException);
           }
            scope.ScopeComitting -= OnScopeCommitting;
            scope.ScopeRollingback -= OnScopeRollingBack;
            scope.Complete();
            _attachedScopes.Remove(scope);
            if (_attachedScopes.Count == 0)
            {
                _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_OnScopeCommitting_Committing, _transactionId));
                try
                {
                    _unitOfWork.Flush();
                    _transaction.Complete();
                }
                finally
                {
                    Dispose(); //Dispose the transaction after comitting.
                }
            }
        }

        /// <summary>
        /// Callback executed when an enlisted scope is rolledback.
        /// </summary>
        void OnScopeRollingBack(IUnitOfWorkScope scope)
        {
            Guard.Against<ObjectDisposedException>(_disposed, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_UnitOfWorkTransaction_OnScopeRollingBack_ObjectDisposedException);
            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_OnScopeRollingBack_Signalled, scope.ScopeId, _transactionId));
            _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_OnScopeRollingBack_Disposing, _transactionId));

            scope.ScopeComitting -= OnScopeCommitting;
            scope.ScopeRollingback -= OnScopeRollingBack;
            scope.Complete();
            _attachedScopes.Remove(scope);
            Dispose();
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

        void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _logger.Info(x => x(DeploySoftware_LaunchPad_Shared_Common_Resources.Info_UnitOfWorkTransaction_Dispose_Disposing, _transactionId));
                if (_unitOfWork != null)
                    _unitOfWork.Dispose();

                if (_transaction != null)
                    _transaction.Dispose();

                if (TransactionDisposing != null)
                    TransactionDisposing(this);

                if (_attachedScopes != null && _attachedScopes.Count > 0)
                {
                    _attachedScopes.ForEach(scope =>
                    {
                        scope.ScopeComitting -= OnScopeCommitting;
                        scope.ScopeRollingback -= OnScopeRollingBack;
                        scope.Complete();
                    });
                    _attachedScopes.Clear();     
                }
            }
            TransactionDisposing = null;
            _unitOfWork = null;
            _transaction = null;
            _attachedScopes = null;
            _disposed = true;
        }
    }
}
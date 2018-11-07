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

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    using DeploySoftware.LaunchPad.Common.Domain.Entities;
    using System;

    /// <summary>
    ///  Base type containing a collection of methods to modify a repository (add, edit, or delete).
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public abstract partial class WriteableRepositoryMethodsBase<TLaunchPadObject> : RepositoryMethodsBase<TLaunchPadObject>, IWriteableRepositoryMethods<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {

        // Best Practice: Use System.EventHandler<T> instead of manually creating new event delegates

        /// <summary>
        /// Raised when a data object is being added to the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> AddingDataObject;

        /// <summary>
        /// Raised when a data object has been added to the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> AddedDataObject;

        /// <summary>
        /// Raised when a data object is being saved to the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> SavingDataObject;

        /// <summary>
        /// Raised when a data object has been saved to the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> SavedDataObject;

        /// <summary>
        /// Raised when a data object is being deleted from the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> DeletingDataObject;

        /// <summary>
        /// Raised when a data object has been deleted from the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> DeletedDataObject;

        /// <summary>  
        /// Raises the <see cref="AddingDataObject">AddingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnAddingDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            AddingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="AddedDataObject">AddedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnAddedDataObject(RepositoryEventArgs e)
        {
            AddedDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="SavingDataObject">SavingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnSavingDataObject(RepositoryEventArgs e)
        {
            SavingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="SavedDataObject">SavedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnSavedDataObject(RepositoryEventArgs e)
        {
            SavedDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="DeletingDataObject">DeletingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnDeletingDataObject(RepositoryEventArgs e)
        {
            DeletingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="DeletedDataObject">DeletedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnDeletedDataObject(RepositoryEventArgs e)
        {
            DeletedDataObject?.Invoke(this, e);
        }

        #region IWriteableRepositoryMethods<TEntity> Members


        /// <summary>
        /// Adds a new data object instance to the repository.
        /// </summary>
        /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be saved
        /// to the database.</param>
        /// <remarks>Implementors of this method must handle the Insert scenario.</remarks>
        public abstract TLaunchPadObject Add(TLaunchPadObject repositoryObject);

        /// <summary>
        /// Marks the changes of an existing data object to be saved to the repository.
        /// </summary>
        /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be
        /// updated in the database.</param>
        public abstract TLaunchPadObject Save(TLaunchPadObject repositoryObject);

        /// <summary>
        /// Marks the changes of an existing data object to be saved to the repository.
        /// </summary>
        /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be
        /// updated in the database.</param>
        /// <remarks>Implementors of this method must handle the Delete scenario. </remarks>
        public abstract void Delete(TLaunchPadObject repositoryObject);

        #endregion
    }
}

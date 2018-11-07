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
    using System;
    /// <summary>
    /// Defines the functionality for any repository whose data can be modified (added, edited, or deleted).
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public interface IWriteableRepositoryMethods<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {

        /// <summary>
        /// Raised when a data object is being added to the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> AddingDataObject;

        /// <summary>
        /// Raised when a data object has been added to the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> AddedDataObject;

        /// <summary>
        /// Raised when a data object is being saved to the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> SavingDataObject;

        /// <summary>
        /// Raised when a data object has been saved to the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> SavedDataObject;

        /// <summary>
        /// Raised when a data object is being deleted from the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> DeletingDataObject;

        /// <summary>
        /// Raised when a data object has been deleted from the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> DeletedDataObject;

        /// <summary>
        /// Adds a new data object instance to the repository.
        /// </summary>
        /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be saved
        /// to the database.</param>
        /// <returns>Returns the added object</returns>
        /// <remarks>Implementors of this method must handle the Insert scenario.</remarks>
        TLaunchPadObject Add(TLaunchPadObject repositoryObject);

        /// <summary>
        /// Marks the changes of an existing data object to be saved to the repository.
        /// </summary>
        /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be
        /// updated in the database.</param>
        /// <returns>Returns the saved object</returns>
        TLaunchPadObject Save(TLaunchPadObject repositoryObject);

        /// <summary>
        /// Marks the changes of an existing data object to be saved to the repository.
        /// </summary>
        /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be
        /// updated in the database.</param>
        /// <remarks>Implementors of this method must handle the Delete scenario. </remarks>
        void Delete(TLaunchPadObject repositoryObject);
    }
}

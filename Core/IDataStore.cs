using System.Collections.Generic;

namespace TipTracker.Core
{
    /// <summary>
    /// Defines a collection of methods for working with stored object of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of object handled by the instance.</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Gets all records in the in the data store.
        /// </summary>
        /// <returns>The collection of records in the store.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Adds a new record to the store.
        /// </summary>
        /// <param name="newItem">The item to add.</param>
        void Add(T newItem);

        /// <summary>
        /// Updates a record in the store.
        /// </summary>
        /// <param name="existing">The instance to update.</param>
        /// <param name="newValue">An instance containing updated values.</param>
        void Update(T existing, T newValue);

        /// <summary>
        /// Deletes an item from the store.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        void Delete(T item);
    }
}

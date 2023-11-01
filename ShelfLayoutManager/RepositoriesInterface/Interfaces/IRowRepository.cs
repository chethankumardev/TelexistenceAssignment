using System;
using ShelfLayoutManager.Entity;

namespace ShelfLayoutManager.RepositoriesInterface.Interfaces
{
    /// <summary>
    /// Represents a repository for managing row data.
    /// </summary>
    public interface IRowRepository
    {
        /// <summary>
        /// Gets all rows asynchronously.
        /// </summary>
        /// <returns>A collection of row entities.</returns>
        Task<IEnumerable<Row>> GetAllRowsAsync();

        /// <summary>
        /// Gets a row by its unique identifier asynchronously.
        /// </summary>
        /// <param name="rowId">The unique identifier of the row.</param>
        /// <returns>A row entity.</returns>
        Task<Row> GetRowByIdAsync(int rowId);

        /// <summary>
        /// Creates a new row asynchronously.
        /// </summary>
        /// <param name="row">The row entity to create.</param>
        Task CreateRowAsync(Row row);

        /// <summary>
        /// Updates a row asynchronously.
        /// </summary>
        /// <param name="row">The updated row entity.</param>
        Task UpdateRowAsync(Row row);

        /// <summary>
        /// Deletes a row by its unique identifier asynchronously.
        /// </summary>
        /// <param name="rowId">The unique identifier of the row to delete.</param>
        /// <returns>True if the row was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteRowAsync(int rowId);
    }
}


using System;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;

namespace ShelfLayoutManager.RepositoriesInterface.Interfaces
{
    /// <summary>
    /// Represents a repository for managing cabinet data.
    /// </summary>
    public interface ICabinetRepository
	{
        /// <summary>
        /// Gets all cabinets asynchronously.
        /// </summary>
        /// <returns>A collection of cabinet models.</returns>
        Task<IEnumerable<CabinetModel>> GetCabinetsAsync();

        /// <summary>
        /// Gets a cabinet by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the cabinet.</param>
        /// <returns>A cabinet model.</returns>
        Task<CabinetModel> GetCabinetAsync(int id);

        /// <summary>
        /// Creates a new cabinet asynchronously.
        /// </summary>
        /// <param name="cabinet">The cabinet entity to create.</param>
        /// <returns>The created cabinet model.</returns>
        Task<CabinetModel> CreateCabinetAsync(Cabinet cabinet);

        /// <summary>
        /// Updates a cabinet asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the cabinet to update.</param>
        /// <param name="cabinet">The updated cabinet model.</param>
        /// <returns>The updated cabinet model.</returns>
        Task<CabinetModel> UpdateCabinetAsync(int id, CabinetModel cabinet);

        /// <summary>
        /// Deletes a cabinet by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the cabinet to delete.</param>
        /// <returns>True if the cabinet was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteCabinetAsync(int id);
    }
}


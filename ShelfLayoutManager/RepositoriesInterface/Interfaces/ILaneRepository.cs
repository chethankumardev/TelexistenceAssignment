using System;
using ShelfLayoutManager.Entity;

namespace ShelfLayoutManager.RepositoriesInterface.Interfaces
{
    /// <summary>
    /// Represents a repository for managing lane data.
    /// </summary>
    public interface ILaneRepository
    {
        /// <summary>
        /// Gets all lanes asynchronously.
        /// </summary>
        /// <returns>A collection of lane entities.</returns>
        Task<IEnumerable<Lane>> GetAllLanesAsync();

        /// <summary>
        /// Gets a lane by its unique identifier asynchronously.
        /// </summary>
        /// <param name="laneId">The unique identifier of the lane.</param>
        /// <returns>A lane entity.</returns>
        Task<Lane> GetLaneByIdAsync(int laneId);

        /// <summary>
        /// Creates a new lane asynchronously.
        /// </summary>
        /// <param name="lane">The lane entity to create.</param>
        Task CreateLaneAsync(Lane lane);

        /// <summary>
        /// Updates a lane asynchronously.
        /// </summary>
        /// <param name="lane">The updated lane entity.</param>
        Task UpdateLaneAsync(Lane lane);

        /// <summary>
        /// Deletes a lane by its unique identifier asynchronously.
        /// </summary>
        /// <param name="laneId">The unique identifier of the lane to delete.</param>
        /// <returns>True if the lane was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteLaneAsync(int laneId);
    }
}


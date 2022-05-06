using ArtPortfolio.Models;

namespace ArtPortfolio.Contracts
{
    public interface IProjectBioRepository
    {
        /// <summary>
        /// Gets all the saved bios.
        /// </summary>
        /// <param name="token">Cancellation token</param>
        /// <returns>A list of biographies</returns>
        public Task<IEnumerable<ProjectBiography>> GetAllBios(CancellationToken token);
        /// <summary>
        /// Gets a bio by the id.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Specified bio</returns>
        public Task<ProjectBiography> GetBioById(Guid id, CancellationToken token);
        /// <summary>
        /// Creates a new bio
        /// </summary>
        /// <param name="bio">A ProjectBiography model</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Newly created bio</returns>
        public Task<ProjectBiography> CreateBio(ProjectBiography bio, CancellationToken token);
        /// <summary>
        /// Updates an existing bio
        /// </summary>
        /// <param name="bio">ProjectBiography model</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>The updated bio.</returns>
        public Task<ProjectBiography> UpdateBio(ProjectBiography bio, CancellationToken token);
        /// <summary>
        /// Deletes an existing bio
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>A boolean</returns>
        public Task<bool> DeleteBio(Guid id, CancellationToken token);
    }
}

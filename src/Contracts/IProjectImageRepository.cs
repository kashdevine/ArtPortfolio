using ArtPortfolio.Models;

namespace ArtPortfolio.Contract
{
    /// <summary>
    /// Interface for managing project images.
    /// </summary>
    public interface IProjectImageRepository
    {
        /// <summary>
        /// Gets a list of project images from the DB.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A list of project images.</returns>
        public Task<IEnumerable<ProjectImage>> GetImagesAsync(CancellationToken token = default);
        /// <summary>
        /// Gets the project image specified by the id.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The project image with matching id.</returns>
        public Task<ProjectImage> GetImageAsync(Guid id, CancellationToken token = default);
        /// <summary>
        /// Creates a new project image in the DB.
        /// </summary>
        /// <param name="image">A project image object.</param>
        /// <param name="token">Canellation token.</param>
        /// <returns>Newly created project image.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public Task<ProjectImage> CreateProjectImage(ProjectImage image, CancellationToken token = default);
        /// <summary>
        /// Updates a project image.
        /// </summary>
        /// <param name="image">A project image object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated project image.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public Task<ProjectImage> UpdateImageAsync(ProjectImage image, CancellationToken token = default);
        /// <summary>
        /// Deletes a project id in the DB.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A boolean.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<bool> DeleteImageAsync(Guid id, CancellationToken token = default);
    }
}

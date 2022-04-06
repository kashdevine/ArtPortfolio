using ArtPortfolio.Models;

namespace ArtPortfolio.Contracts
{
    public interface IProjectVideoRepository
    {
        /// <summary>
        /// Gets an enumerable collection of project videos.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumerable of project videos.</returns>
        public Task<IEnumerable<ProjectVideo>> GetProjectVideosAsync(CancellationToken token = default);
        /// <summary>
        /// Gets a specific project video based on the id.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>ProjectVideo Object.</returns>
        public Task<ProjectVideo> GetProjectVideoAsync(Guid id, CancellationToken token = default);
        /// <summary>
        /// Creates a new project video entry.
        /// </summary>
        /// <param name="projectVideo">ProjectVideo Object</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Newly created projectvideo object.</returns>
        public Task<ProjectVideo> CreateProjectVideo(ProjectVideo projectVideo, CancellationToken token = default);
        /// <summary>
        /// Updates an existing project video entry.
        /// </summary>
        /// <param name="projectVideo">ProjectVideo Object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Updated project video.</returns>
        public Task<ProjectVideo> UpdateProjectVideo(ProjectVideo projectVideo, CancellationToken token = default);
        /// <summary>
        /// Deletes an existing project video.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A boolean based on the outcome.</returns>
        public Task<bool> DeleteProjectVideo(Guid id, CancellationToken token = default);
    }
}

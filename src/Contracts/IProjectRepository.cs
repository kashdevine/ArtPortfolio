using ArtPortfolio.Models;

namespace ArtPortfolio.Contracts
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Gets a collection of project objects.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>IEnumerable of project object.</returns>
        public Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token = default);
        /// <summary>
        /// Gets a project object based on the id.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A project object.</returns>
        public Task<Project> GetProjectAsync(Guid id, CancellationToken token = default);
        /// <summary>
        /// Creates a new project entity.
        /// </summary>
        /// <param name="project">A project object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The newly created project.</returns>
        public Task<Project> CreateProject(Project project, CancellationToken token = default);
        /// <summary>
        /// Updates an existing project entity.
        /// </summary>
        /// <param name="project">A project object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The updated project.</returns>
        public Task<Project> UpdateProject(Project project, CancellationToken token = default);
        /// <summary>
        /// Deletes an existing project entity if it exists.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> DeleteProjectAsync(Guid id, CancellationToken token = default);

    }
}

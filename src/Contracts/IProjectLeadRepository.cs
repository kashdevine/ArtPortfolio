using ArtPortfolio.Models;

namespace ArtPortfolio.Contracts
{
    public interface IProjectLeadRepository
    {
        /// <summary>
        /// Get all the leads.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>IEnumerable of project leads.</returns>
        public Task<IEnumerable<ProjectLead>> GetAllLeads(CancellationToken token = default);

        /// <summary>
        /// Gets a lead based on the id.
        /// </summary>
        /// <param name="id">A Guid.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A project lead</returns>
        public Task<ProjectLead> GetLeadById(Guid id, CancellationToken token = default);

        /// <summary>
        /// Creates a new project lead
        /// </summary>
        /// <param name="projectLead">A Project Lead</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Newly created project lead</returns>
        public Task<ProjectLead> CreateLead(ProjectLead projectLead, CancellationToken token = default);

        /// <summary>
        /// Updates an existing project lead
        /// </summary>
        /// <param name="projectLead">A Project Lead</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Updated project lead</returns>
        public Task<ProjectLead> UpdateLead(ProjectLead projectLead, CancellationToken token = default);

        /// <summary>
        /// Deletes a project based on the id.
        /// </summary>
        /// <param name="id">A Guid</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>A bool</returns>
        public Task<bool> DeleteLead(Guid id, CancellationToken token = default);
    }
}

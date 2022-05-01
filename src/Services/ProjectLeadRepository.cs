using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IProjectLeadRepository"/>
    /// </summary>
    public class ProjectLeadRepository : IProjectLeadRepository
    {
        private readonly ArtPortfolioDbContext _ctx;

        public ProjectLeadRepository(ArtPortfolioDbContext ctx)
        {
            _ctx = ctx;
        }
        public Task<ProjectLead> CreateLead(ProjectLead projectLead, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLead(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectLead>> GetAllLeads(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectLead> GetLeadById(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectLead> UpdateLead(ProjectLead projectLead, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}

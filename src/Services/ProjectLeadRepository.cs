using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ProjectLead> CreateLead(ProjectLead projectLead, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            if (projectLead == null) 
            { 
                throw new ArgumentNullException(nameof(projectLead)); 
            }
            if (projectLead.Message == null)
            {
                throw new ArgumentNullException(nameof(projectLead), "The lead message cannot be null.");
            }
            await _ctx.ProjectLeads.AddAsync(projectLead);
            await Save(token);
            return projectLead;
        }

        public async Task<bool> DeleteLead(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            var deletedLead = await _ctx.ProjectLeads.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (deletedLead == null)
            {
                return await Save(token);
            }
            _ctx.Remove(deletedLead);
            return await Save(token);
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

        private async Task<bool> Save(CancellationToken token = default)
        {
            return await _ctx.SaveChangesAsync(token) > 0; 
        }
    }
}

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
            var emailExist = await _ctx.ProjectLeads.AnyAsync(x => x.Email == projectLead.Email);

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
            if (emailExist)
            {
                throw new Exception(String.Format("{0} already exists.", projectLead.Email));
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

        public async Task<IEnumerable<ProjectLead>> GetAllLeads(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return await _ctx.ProjectLeads.Where(x => x.Email != null).ToListAsync();
        }

        public async Task<ProjectLead> GetLeadById(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return await _ctx.ProjectLeads.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProjectLead> UpdateLead(ProjectLead projectLead, CancellationToken token = default)
        {
            var leadExists = await _ctx.ProjectLeads.AnyAsync(x => x.Id == projectLead.Id);
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            if (!leadExists)
            {
                throw new Exception(String.Format("Lead with email {0} doesn't exist", projectLead.Email));
            }
            _ctx.ProjectLeads.Update(projectLead);
            await Save(token);
            return projectLead;
        }

        private async Task<bool> Save(CancellationToken token = default)
        {
            return await _ctx.SaveChangesAsync(token) > 0; 
        }
    }
}

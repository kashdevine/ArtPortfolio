using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IProjectBioRepository"/>
    /// </summary>
    public class ProjectBioRepository : IProjectBioRepository
    {
        private readonly ArtPortfolioDbContext _ctx;
        public ProjectBioRepository(ArtPortfolioDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<ProjectBiography> CreateBio(ProjectBiography bio, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            await _ctx.ProjectBiographies.AddAsync(bio);
            await Save(token);
            return bio;
        }

        public async Task<bool> DeleteBio(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            var deletedBio = await _ctx.ProjectBiographies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (deletedBio == null)
            {
                return await Save();
            }
            _ctx.ProjectBiographies.Remove(deletedBio);
            return await Save();
        }

        public async Task<IEnumerable<ProjectBiography>> GetAllBios(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return await _ctx.ProjectBiographies.Where(x => x.Body != null).ToListAsync();
        }

        public Task<ProjectBiography> GetBioById(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectBiography> UpdateBio(ProjectBiography bio, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> Save(CancellationToken token = default)
        {
            return await _ctx.SaveChangesAsync(token) > 0;
        }
    }
}

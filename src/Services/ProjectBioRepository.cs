using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;

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

        public Task<bool> DeleteBio(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectBiography>> GetAllBios(CancellationToken token = default)
        {
            throw new NotImplementedException();
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

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
        public Task<ProjectBiography> CreateBio(ProjectBiography bio, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBio(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectBiography>> GetAllBios(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectBiography> GetBioById(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectBiography> UpdateBio(ProjectBiography bio, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

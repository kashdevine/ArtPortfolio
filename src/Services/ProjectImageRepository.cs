using ArtPortfolio.Contract;
using ArtPortfolio.Data;
using ArtPortfolio.Models;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IProjectImageRepository"/>
    /// </summary>
    public class ProjectImageRepository : IProjectImageRepository
    {
        private readonly ArtPortfolioDbContext _ctx;
        public ProjectImageRepository(ArtPortfolioDbContext ctx)
        {
            _ctx = ctx;
        }
        public Task<ProjectImage> CreateProjectImage(ProjectImage image, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImageAsync(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectImage> GetImageAsync(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectImage>> GetImagesAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectImage> UpdateImageAsync(ProjectImage image, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

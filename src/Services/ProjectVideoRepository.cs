using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;

namespace ArtPortfolio.Services
{
    public class ProjectVideoRepository : IProjectVideoRepository
    {
        private readonly ArtPortfolioDbContext _ctx;
        public ProjectVideoRepository(ArtPortfolioDbContext ctx)
        {
            _ctx = ctx;
        }
        public Task<ProjectVideo> CreateProjectVideo(ProjectVideo projectVideo, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProjectVideo(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectVideo> GetProjectVideoAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectVideo>> GetProjectVideosAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectVideo> UpdateProjectVideo(ProjectVideo projectVideo, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}

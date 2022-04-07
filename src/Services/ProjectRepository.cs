using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IProjectRepository"/>
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly ArtPortfolioDbContext _ctx;
        public ProjectRepository(ArtPortfolioDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Project> CreateProject(Project project, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProjectAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Project> UpdateProject(Project project, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}

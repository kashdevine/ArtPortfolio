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

        public async Task<Project> CreateProject(Project project, CancellationToken token = default)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            _ctx.Projects.AddAsync(project);

            var isSaved = await Save(token);
            if (!isSaved)
            {
                throw new Exception(String.Format("Could not create the project at {0}", nameof(CreateProject)));
            }

            return project;
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

        private async Task<bool> Save(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            return await _ctx.SaveChangesAsync(token) > 0;
        }
    }
}

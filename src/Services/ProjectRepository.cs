using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken token = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            var projectToDelete = await _ctx.Projects.FirstOrDefaultAsync(p=> p.Id == id);
            if (projectToDelete == null)
            {
                return false;
            }
            _ctx.Projects.Remove(projectToDelete);
            return await Save(token);
        }

        public async Task<Project> GetProjectAsync(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return await _ctx.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return await _ctx.Projects.Where(p => p.Name != String.Empty).ToListAsync();
        }

        public async Task<Project> UpdateProject(Project project, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            _ctx.Projects!.Update(project);

            var saved = await Save(token);
            if (!saved)
            {
                throw new Exception(String.Format("Could not update the project at {0}.", nameof(UpdateProject)));
            }
            return project;
        }

        /// <summary>
        /// Saves the changes to the db.
        /// </summary>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>A bool.</returns>
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

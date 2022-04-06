using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IProjectVideoRepository"/>
    /// </summary>
    public class ProjectVideoRepository : IProjectVideoRepository
    {
        private readonly ArtPortfolioDbContext _ctx;
        public ProjectVideoRepository(ArtPortfolioDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<ProjectVideo> CreateProjectVideoAsync(ProjectVideo projectVideo, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
               token.ThrowIfCancellationRequested();
            }
            await _ctx.ProjectVideos.AddAsync(projectVideo);
            if (!await Save(token))
            {
                throw new Exception(String.Format("Could not create project video at {0}", nameof(CreateProjectVideoAsync)));
            }
            return projectVideo;
        }

        public async Task<bool> DeleteProjectVideoAsync(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            var doesExistObject = await _ctx.ProjectVideos.FirstOrDefaultAsync(x => x.Id == id);
            if (doesExistObject == null)
            {
                throw new Exception(String.Format("A video with id {0} does not exist", id));
            }
            
            _ctx.Remove(doesExistObject);
            return await Save(token);
        }

        public async Task<ProjectVideo> GetProjectVideoAsync(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return await _ctx.ProjectVideos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ProjectVideo>> GetProjectVideosAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            return await _ctx.ProjectVideos.Where(x => x.Id != null).ToListAsync();
        }

        public async Task<ProjectVideo> UpdateProjectVideoAsync(ProjectVideo projectVideo, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            if (! await _ctx.ProjectVideos.AnyAsync(x => x.Id == projectVideo.Id))
            {
                throw new Exception(String.Format("Could not update video at {0}", nameof(UpdateProjectVideoAsync)));
            }
            _ctx.ProjectVideos.Update(projectVideo);
            await Save(token);

            return projectVideo;
        }

        /// <summary>
        /// Saves pending changes.
        /// </summary>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>A boolean.</returns>
        private async Task<bool> Save(CancellationToken token = default)
        {
            return await _ctx.SaveChangesAsync(token) > 0;
        }
    }
}

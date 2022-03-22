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
        public async Task<ProjectImage> CreateProjectImage(ProjectImage image, CancellationToken token = default)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            await _ctx.ProjectImages.AddAsync(image);

            var isSaved = await Save(token);

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            if (!isSaved)
            {
                throw new Exception();
            }

            return image;
        }

        public Task<bool> DeleteImageAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectImage> GetImageAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectImage>> GetImagesAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectImage> UpdateImageAsync(ProjectImage image, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the current changes to the database.
        /// </summary>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>A bool.</returns>
        private async Task<bool> Save(CancellationToken token = default)
        {
            return await _ctx.SaveChangesAsync(token) > 0;
        }
    }
}

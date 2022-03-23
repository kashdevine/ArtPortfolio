using ArtPortfolio.Contract;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

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
                throw new Exception(String.Format("Could not create {0} in database.", image));
            }

            return image;
        }

        public async Task<bool> DeleteImageAsync(Guid id, CancellationToken token = default)
        {
            var objToDelete = await _ctx.ProjectImages.FirstAsync(img => img.Id == id);

            if (objToDelete == null)
            {
                return false;
            }

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            _ctx.ProjectImages.Remove(objToDelete);
            return await Save(token);
        }

        public async Task<ProjectImage> GetImageAsync(Guid id, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            return await _ctx.ProjectImages.FirstOrDefaultAsync(img => img.Id == id);
        }

        public async Task<IEnumerable<ProjectImage>> GetImagesAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            return _ctx.ProjectImages.Where(pi => pi.Name != "");
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

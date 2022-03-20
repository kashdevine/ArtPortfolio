using ArtPortfolio.Models;

namespace ArtPortfolio.Contract
{
    /// <summary>
    /// Interface for managing project images.
    /// </summary>
    public interface IProjectImageRepository
    {
        /// <summary>
        /// Gets a list of project images from the DB.
        /// </summary>
        /// <returns>A list of project images.</returns>
        public Task<IEnumerable<ProjectImage>> GetImagesAsync();
    }
}

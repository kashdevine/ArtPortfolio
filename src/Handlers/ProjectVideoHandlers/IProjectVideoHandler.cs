using ArtPortfolio.Models;

namespace ArtPortfolio.Handlers.ProjectVideoHandlers
{
    public interface IProjectVideoHandler
    {
        /// <summary>
        /// Saves a video to the disk.
        /// </summary>
        /// <param name="file">IFormfile object.</param>
        /// <param name="video">ProjectVideo object.</param>
        /// <returns>A boolean.</returns>
        public bool SaveVideo(IFormFile file, ProjectVideo video);
        /// <summary>
        /// Deletes a video from the disk.
        /// </summary>
        /// <param name="video">ProjectVideo object.</param>
        /// <returns>A boolean.</returns>
        public bool DeleteVideo(ProjectVideo video);
    }
}

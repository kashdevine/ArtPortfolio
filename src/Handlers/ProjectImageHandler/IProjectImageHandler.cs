using ArtPortfolio.Models;

namespace ArtPortfolio.Handlers.ProjectImageHandler
{
    public interface IProjectImageHandler
    {
        /// <summary>
        /// Gets the image location as a string for the front end.
        /// </summary>
        /// <param name="image">Project Image object</param>
        /// <returns>A string with the location of the image.</returns>
        public string GetImagePath(ProjectImage image);
        /// <summary>
        /// Saves the image to the disc. The IFormfile is null but
        /// it's not optional if we want to save the image properly.
        /// Also sets the full path property for the Project Image.
        /// </summary>
        /// <param name="imageFile">IFormfile Object</param>
        /// <param name="image">Project Image</param>
        /// <returns>A boolean.</returns>
        public Task<bool> SaveImage(ProjectImage image, IFormFile? imageFile = null);
        /// <summary>
        /// Deletes the image from the disc.
        /// </summary>
        /// <param name="imageFile">IFromfile object</param>
        /// <param name="image">Project Image object</param>
        /// <returns>A boolean.</returns>
        public bool DeleteImage(IFormFile imageFile, ProjectImage image);
    }
}

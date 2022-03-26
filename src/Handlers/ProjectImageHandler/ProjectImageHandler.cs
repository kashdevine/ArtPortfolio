using ArtPortfolio.Models;
using System.IO.Pipelines;

namespace ArtPortfolio.Handlers.ProjectImageHandler
{
    /// <summary>
    /// <inheritdoc cref="IProjectImageHandler"/>
    /// </summary>
    public class ProjectImageHandler : IProjectImageHandler
    {
        private HashSet<string> _imagetypes = new HashSet<string>() { "image/png", "image/jpeg", "image/jpg", "image/svg+xml"};

        private readonly IWebHostEnvironment _env;

        public ProjectImageHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public Task<bool> DeleteImage(IFormFile imageFile, ProjectImage image)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetImagePath(ProjectImage image)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveImage(ProjectImage image, string fileName, IFormFile imageFile)
        {
            if (imageFile == null) { throw new ArgumentNullException(nameof(imageFile)); };

            var fileExtension = Path.GetExtension(fileName);
            var finalFileName = String.Format("{0}{1}", image.Id, fileExtension);

            var imageDirPath = Path.Combine(_env.ContentRootPath, image.ProjectId.ToString());

            var imageFilePath = Path.Combine(imageDirPath, finalFileName);

            if (!Directory.Exists(imageDirPath)) 
            {
                Directory.CreateDirectory(imageDirPath);
            }

            using(var fs = new FileStream(imageFilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                imageFile.CopyToAsync(fs);
            }
            return true;
        }
    }
}

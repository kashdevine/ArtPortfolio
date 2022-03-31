using ArtPortfolio.Models;
using System.IO.Pipelines;

namespace ArtPortfolio.Handlers.ProjectImageHandler
{
    /// <summary>
    /// <inheritdoc cref="IProjectImageHandler"/>
    /// </summary>
    public class ProjectImageHandler : IProjectImageHandler
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public ProjectImageHandler(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public bool DeleteImage(IFormFile imageFile, ProjectImage image)
        {
            if (imageFile == null) { throw new ArgumentNullException(nameof(imageFile)); };
            var imageDir = _config.GetValue<string>("ImageFileDir");


            var imageDirPath = Path.Combine(_env.ContentRootPath, imageDir, image.ProjectId.ToString());

            if (Directory.Exists(imageDirPath))
            {
                var projectFiles = Directory.EnumerateFiles(imageDirPath);
                foreach (var projectFile in projectFiles)
                {
                    if (projectFile.StartsWith(image.Id.ToString()))
                    {
                        Directory.Delete(projectFile);
                    }
                }
            }

            return true; 
        }

        public string GetImagePath(ProjectImage image)
        {
            var imageDir = _config.GetValue<string>("ImageFileDir");

            var imageDirPath = Path.Combine(_env.ContentRootPath, imageDir, image.ProjectId.ToString());

            if (Directory.Exists(imageDirPath))
            {
                var projectFiles = Directory.EnumerateFiles(imageDirPath);
                foreach (var projectFile in projectFiles)
                {
                    if (projectFile.Contains(image.Id.ToString()))
                    {
                        return Path.GetFullPath(projectFile);
                    }
                }
            }
            return null;
        }

        public async Task<bool> SaveImage(ProjectImage image, string fileName, IFormFile? imageFile)
        {
            if (imageFile == null) { throw new ArgumentNullException(nameof(imageFile)); };
            var imageDir = _config.GetValue<string>("ImageFileDir");

            var fileExtension = Path.GetExtension(fileName);
            var finalFileName = String.Format("{0}{1}", image.Id, fileExtension);

            var imageDirPath = Path.Combine(_env.ContentRootPath, imageDir, image.ProjectId.ToString());
            var imageFilePath = Path.Combine(imageDirPath, finalFileName);

            if (!Directory.Exists(imageDirPath)) 
            {
                Directory.CreateDirectory(imageDirPath);
            }


            using(var fs = new FileStream(imageFilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await imageFile.CopyToAsync(fs);
                image.FullFilePath = imageFilePath;
            }

            return true;
        }
    }
}

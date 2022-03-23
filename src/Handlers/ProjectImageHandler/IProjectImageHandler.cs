using ArtPortfolio.Models;

namespace ArtPortfolio.Handlers.ProjectImageHandler
{
    public interface IProjectImageHandler
    {
        public Task<string> GetImage(ProjectImage image);
        public Task<bool> SaveImage(IFormFile file, ProjectImage image);
        public Task<bool> UpdateImage(IFormFile file, ProjectImage image);
        public Task<bool> DeleteImage(IFormFile file, ProjectImage image);
    }
}

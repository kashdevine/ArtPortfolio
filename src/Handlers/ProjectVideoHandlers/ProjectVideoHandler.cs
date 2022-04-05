using ArtPortfolio.Models;

namespace ArtPortfolio.Handlers.ProjectVideoHandlers
{
    public class ProjectVideoHandler : IProjectVideoHandler
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        public ProjectVideoHandler(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }
        public bool DeleteVideo(ProjectVideo video)
        {
            if (video.FullFilePath == null)
            {
                return false;
            }

            File.Delete(video.FullFilePath);

            return true;
        }

        public bool SaveVideo(IFormFile file, ProjectVideo video)
        {
            var vidDir = _config.GetValue<string>("VideoFileDir");

            var fileExtension = Path.GetExtension(file.FileName);
            var finalFileName = Path.Combine(String.Format("{0}{1}", video.Id,fileExtension));

            var vidDirPath = Path.Combine(_env.ContentRootPath, vidDir, video.ProjectId.ToString());
            var vidFilePath = Path.Combine(vidDirPath, finalFileName);

            if (!Directory.Exists(vidDirPath))
            {
                Directory.CreateDirectory(vidDirPath);
            }

            using(var fs = new FileStream(vidFilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                file.CopyTo(fs);
                video.FullFilePath = vidFilePath;
            }

            return true;
        }
    }
}

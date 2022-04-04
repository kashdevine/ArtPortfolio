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
            throw new NotImplementedException();
        }

        public bool SaveVideo(IFormFile file, ProjectVideo video)
        {
            throw new NotImplementedException();
        }
    }
}

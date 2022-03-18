namespace ArtPortfolio.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MainPhotoName { get; set; }
        public IEnumerable<ProjectImage>? ProjectImages { get; set; }
        public IEnumerable<ProjectVideo>? ProjectVideos { get; set; }
    }
}

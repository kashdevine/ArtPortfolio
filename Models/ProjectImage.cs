namespace ArtPortfolio.Models
{
    public class ProjectImage
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}

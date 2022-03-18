namespace ArtPortfolio.Models
{
    public class ProjectVideo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LinkURI { get; set; }
        public string Description { get; set; }
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}

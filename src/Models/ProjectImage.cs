namespace ArtPortfolio.Models
{
    public class ProjectImage
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? AltTitle { get; set; }
        public string? Description { get; set; }
        public string? FullFilePath { get; set; }
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}

namespace ArtPortfolio.Models
{
    public class ProjectBiography
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public bool Selected { get; set; } = false;
    }
}

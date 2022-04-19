using Microsoft.AspNetCore.Identity;

namespace ArtPortfolio.Models
{
    public class ProjectUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

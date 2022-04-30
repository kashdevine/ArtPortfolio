using System.ComponentModel.DataAnnotations;

namespace ArtPortfolio.Models
{
    public class ProjectLead
    {
        public Guid Id { get; set; }

        [MaxLength(25, ErrorMessage = "First Name can only be up to 25 characters.")]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Last Name can only be up to 25 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsSpam { get; set; }
        public bool ForwardedToEmail { get; set; }
        public DateTime Created { get; set; }
    }
}


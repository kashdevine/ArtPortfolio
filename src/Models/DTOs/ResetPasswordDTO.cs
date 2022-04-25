namespace ArtPortfolio.Models.DTOs
{
    public class ResetPasswordDTO
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}

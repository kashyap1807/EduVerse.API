namespace EduVerse.API.Dto
{
    public class UpdateUserProfileDto
    {
        public required int UserId { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Picture { get; set; }
    }
}

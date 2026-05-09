namespace Kursach_Backend.Models.DTO
{
    public class UpdatePublicationEditionRequestDto
    {
        public int Id { get; set; }
        public string EditionName { get; set; }
        public string? Description { get; set; }
    }
}

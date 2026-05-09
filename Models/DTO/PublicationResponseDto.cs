namespace Kursach_Backend.Models.DTO
{
    public class PublicationResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? EditionName { get; set; }
        public string PublicationTypeName { get; set; }
        public string? RINC { get; set; }
        public string Type { get; set; }
    }
}

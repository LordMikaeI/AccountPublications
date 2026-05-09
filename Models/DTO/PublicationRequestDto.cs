using System.ComponentModel.DataAnnotations;

namespace Kursach_Backend.Models.DTO
{
    public class PublicationRequestDto
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? PublicationType { get; set; } // ДОБАВИТЬ
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? PublicationEditionId { get; set; }

        [RegularExpression(@"^\d{4}-\d{4}-\d{4}$", ErrorMessage = "Неверный формат РИНЦ. Формат: XXXX-XXXX-XXXX")]
        public string? RINC { get; set; } // Опциональный РИНЦ


    }
}

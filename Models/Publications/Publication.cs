using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kursach_Backend.Models.Employees;

namespace Kursach_Backend.Models.Publications
{
    public class Publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? PublicationEditionId { get; set; }
        public PublicationEdition? PublicationEdition { get; set; }
    }
}
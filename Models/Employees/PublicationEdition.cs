using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kursach_Backend.Models.Publications;

namespace Kursach_Backend.Models.Employees
{
    public class PublicationEdition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EditionName { get; set; }

        public string? Description { get; set; }

        public List<Publication> Publications { get; set; }
    }
}

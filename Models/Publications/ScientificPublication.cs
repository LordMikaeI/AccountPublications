using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursach_Backend.Models.Publications
{
    public class ScientificPublication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RINC { get; set; }

        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}

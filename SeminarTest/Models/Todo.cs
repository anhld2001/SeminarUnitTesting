using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeminarTest.Models
{

    [Table("Todo")]
    public class ToDo
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}

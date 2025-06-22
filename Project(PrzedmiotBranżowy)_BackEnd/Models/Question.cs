using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.Models
{
    [Table("questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [StringLength(256)]
        [Required]
        [Column("title")]
        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey($"Questions_{nameof(TestId)}_FK")]
        [Column("test_id")]
        public int TestId { get; set; }

        public Test Test { get; set; } = null!;

        public List<Answer> Answers { get; } = [];

    }
}

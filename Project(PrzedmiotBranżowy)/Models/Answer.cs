using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.Models
{
    [Table("answers")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        [Column("title")]
        public string Title { get; set; }

        [DefaultValue(false)]
        [Column("is_correct")]
        public bool IsCorrect { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey($"Answers_{nameof(QuestionId)}_FK")]
        [Column("question_id")]
        public int QuestionId { get; set; }

        public Question Question { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Project_PrzedmiotBranzowy_BackEnd.Models
{
    [Table("tests")]
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Column("name")]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("created_at")]
        [AllowNull]
        public DateTime CreatedAt { get; set; }

        [ForeignKey($"Tests_{nameof(AdminId)}_FK")]
        [Column("admin_id")]
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; } = null!;

        public virtual List<Question> Questions { get; set; } = [];

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}

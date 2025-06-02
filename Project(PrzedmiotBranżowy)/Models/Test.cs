using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.Models
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
        public DateTime CreatedAt { get; set; }

        [ForeignKey($"Tests_{nameof(AdminId)}_FK")]
        [Column("admin_id")]
        public int AdminId { get; set; }

        public Admin Admin { get; set; } = null!;

        public List<Question> Questions { get; set; } = [];
    }
}

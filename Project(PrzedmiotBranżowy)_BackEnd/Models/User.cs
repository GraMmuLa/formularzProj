using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PrzedmiotBranżowy_.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("firstname")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        [Column("lastname")]
        public string Lastname { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}

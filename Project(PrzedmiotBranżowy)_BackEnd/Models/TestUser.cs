using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_BackEnd.Models
{
    [Table("tests_users")]
    public class TestUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey($"TestsUsers_{nameof(UserId)}_{nameof(TestId)}_FK")]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey($"TestsUsers_{nameof(TestId)}_{nameof(UserId)}_FK")]
        [Column("test_id")]
        public int TestId { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("created_at")]
        [AllowNull]
        public DateTime CreatedAt;

        public virtual User User { get; set; } = null!;

        public virtual Test Test { get; set; } = null!;
    }
}

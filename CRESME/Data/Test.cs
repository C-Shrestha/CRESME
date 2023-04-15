using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRESME.Data
{
    [Table("Test")]
    public class Test
    {
        public string? NID { get; set; }
        [Required]
        [Key]
        public string? Course { get; set; }
    }
}

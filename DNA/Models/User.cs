using System.ComponentModel.DataAnnotations;

namespace DNA.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

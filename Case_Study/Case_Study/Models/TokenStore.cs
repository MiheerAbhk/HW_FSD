using System.ComponentModel.DataAnnotations;

namespace Case_Study.Models
{
    public class TokenStore
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; } = true;

    }
}

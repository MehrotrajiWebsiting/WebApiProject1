using System.ComponentModel.DataAnnotations;

namespace WebApiProject1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? UserEmail { get; set; }

        public string Password { get; set; } = null!;

        public string? Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

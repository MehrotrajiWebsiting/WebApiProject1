using System.ComponentModel.DataAnnotations;

namespace WebApiProject1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; } = 1;
    }
}

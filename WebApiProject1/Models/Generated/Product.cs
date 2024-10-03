using System.ComponentModel.DataAnnotations;

namespace WebApiProject1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
    }
}

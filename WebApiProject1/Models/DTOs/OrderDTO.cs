using WebApiProject1.Validations;

namespace WebApiProject1.Models
{
    public class OrderDTO
    {
        public string ProductName { get; set; } = null!;

        [Product_QuantityValidation]
        public int Quantity { get; set; } = 1;
    }
}

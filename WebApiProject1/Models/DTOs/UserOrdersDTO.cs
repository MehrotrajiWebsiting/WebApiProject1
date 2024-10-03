namespace WebApiProject1.Models
{
    public class UserOrdersDTO
    {
        public int Id { get; set; }

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; } = 1;

        public decimal Price
        {
            get
            {
                // Calculate the price based on product price and quantity
                return Product != null ? Product.Price * Convert.ToDecimal(Quantity) : 0;
            }
        }
    }
}

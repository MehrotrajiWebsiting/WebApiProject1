namespace WebApiProject1.Models
{
    public static class OrderMapper
    {
        public static UserOrdersDTO ConvertToUserOrdersDTO(Order o)
        {
            return new UserOrdersDTO()
            {
                Id = o.Id,
                Product = o.Product,
                Quantity = o.Quantity,
            };
        }
    }
}

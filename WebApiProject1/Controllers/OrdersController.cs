using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject1.Authorization;
using WebApiProject1.Data;
using WebApiProject1.Models;

namespace WebApiProject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ProjectContext _context;
        public OrdersController(ProjectContext context)
        {
            _context = context;
        }

        //Get my orders
        [HttpGet]
        [UserAuthorizationFilter]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var UserId = HttpContext.Session.GetString("UserId");
            //if (UserId == null)
            //{
            //    return Unauthorized("User is NOT Authenticated");
            //}
            int Id = int.Parse(UserId);

            var user = await _context.Users
                .Include(u => u.Orders) //without this orders will always show null
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync( u => u.Id == Id );

            if (user.Orders.Count == 0) return Ok($"No order yet for user : {user.UserEmail}");

            var orders = user.Orders.Select(o => OrderMapper.ConvertToUserOrdersDTO(o)); 

            return Ok(orders);
        }

        //Create a new Order
        [HttpPost]
        [UserAuthorizationFilter]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO MyOrder)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            int Id = int.Parse(UserId);
            var user = await _context.Users.FindAsync(Id);

            var product = await _context.Products.SingleOrDefaultAsync(
                   p=> p.Name.ToUpper().Equals(MyOrder.ProductName.ToUpper()));

            if (product == null)
                return BadRequest("Product does not exist");

            var order = new Order()
            {
                Product = product,
                Quantity = MyOrder.Quantity,
            };

            user.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get),
                new { id = order.Id },
                OrderMapper.ConvertToUserOrdersDTO(order));
        }
    }
}

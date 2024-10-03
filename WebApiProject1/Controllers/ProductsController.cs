using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject1.Data;
using WebApiProject1.Models;

namespace WebApiProject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProjectContext _context;
        public ProductsController(ProjectContext context)
        {
            _context = context;
        }

        //Get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }
    }
}
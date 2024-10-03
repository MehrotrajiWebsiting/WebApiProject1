using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject1.Data;
using WebApiProject1.Filters;
using WebApiProject1.Filters.ActionFilters;
using WebApiProject1.Models;

namespace WebApiProject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ProjectContext _context;
        public UsersController(ProjectContext context)
        {
            this._context = context;
        }

        //Get Details of User
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return Ok(await _context.Users.Include(u => u.Orders).ThenInclude(o => o.Product).FirstOrDefaultAsync(u=>u.Id==id));
        }

        //Register
        [HttpPost("Register")]
        [User_ValidateUserInputFilter]
        //Service filter to apply the filter
        [ServiceFilter(typeof(User_ValidateExistingUserFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            //If Users table is Empty store 0
            //int maxId = context.Users.Any() ? context.Users.Max(x => x.Id) : 0;

            //User NewUser = new User()
            //{
            //    //Id = maxId + 1, //Identity Variable - Do not update ID
            //    UserEmail = user.UserEmail,
            //    Password = user.Password,
            //    Phone = user.Phone,
            //};
            //context.Users.Add(NewUser);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser),
                new { id = user.Id },
                user);
        }

        // LOGIN - BASIC AUTHENTICATION
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserEmail.Equals(loginDTO.Email));

            if (user == null)
            {
                return Unauthorized("Wrong Username");
            }

            if(user.Password != loginDTO.Password)
            {
                return Unauthorized("Wrong Password");
            }

            // Set session variables for authentication and user identification
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetString("UserId", user.Id.ToString()); // Store the user's ID in the session

            return Ok($"Welcome user : {loginDTO.Email}");
        }
    }
}

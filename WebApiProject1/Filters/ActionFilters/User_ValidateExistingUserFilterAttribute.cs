using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApiProject1.Data;
using WebApiProject1.Models;

namespace WebApiProject1.Filters.ActionFilters
{
    public class User_ValidateExistingUserFilterAttribute : ActionFilterAttribute
    {
        //TO USE THIS CONTEXT, Register this filter in Dependency Injection (in Program.cs)
        private readonly ProjectContext _context;
        public User_ValidateExistingUserFilterAttribute(ProjectContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var user = context.ActionArguments["user"] as User;


            var exisitingUser = _context.Users.Where(u => u.UserEmail == user.UserEmail).FirstOrDefault();

            if (exisitingUser != null)
            {
                context.ModelState.AddModelError("User", "User already exists");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}

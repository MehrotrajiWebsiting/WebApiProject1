using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiProject1.Authorization
{
    //Prevent unauthorized user from sending orders
    public class UserAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.Session.GetString("IsAuthenticated")
                                == "true";
            if (!isAuthenticated)
            {
                context.ModelState.AddModelError("User", "User is not Authenticated");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                context.Result = new UnauthorizedObjectResult(problemDetails);
            }
        }
    }
}

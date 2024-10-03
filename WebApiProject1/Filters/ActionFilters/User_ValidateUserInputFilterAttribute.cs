﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiProject1.Models;

namespace WebApiProject1.Filters.ActionFilters
{
    public class User_ValidateUserInputFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var user = context.ActionArguments["user"] as User;

            if (user == null)
            {
                context.ModelState.AddModelError("User", "User object is Empty");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else if (string.IsNullOrWhiteSpace(user.UserEmail))
            {
                context.ModelState.AddModelError("UserEmail", "User Email is Empty");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            //The password field always gives - PasswordField is required because in Users class
            // public string Password { get; set; } = null!;
            else if (user.Password.Trim().Length < 8)
            {
                context.ModelState.AddModelError("Password", "Password must have more than 8 characters");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else if( !string.IsNullOrWhiteSpace(user.Phone) && ( user.Phone.Trim().Length != 10 || 
                ! int.TryParse(user.Phone,out int pn)
                ))
            {
                context.ModelState.AddModelError("Phone", "Phone number must have 10 digits");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}

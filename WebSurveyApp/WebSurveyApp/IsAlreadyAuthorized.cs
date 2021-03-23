﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace WebSurveyApp
{
    public class IsAlreadyAuthorized : ActionFilterAttribute
    {
        private readonly string _pathString;

        public IsAlreadyAuthorized(string pathString)
        {
            _pathString = pathString;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isAuthorized = context.HttpContext.User.Identity.IsAuthenticated;

            if (isAuthorized)
            {
                context.Result = new RedirectResult(_pathString);
            }
            else
            {
                await next();
            }
        }
    }
}

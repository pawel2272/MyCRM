using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.UI.Filters
{
    public class JwtAuthFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public JwtAuthFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private CookieBuilder CreateAuthorizationCookie(double time)
        {
            CookieBuilder cookie = new CookieBuilder()
            {
                IsEssential = true,
                Expiration = TimeSpan.FromMinutes(time),
                Name = "Authorization"
            };
            return cookie;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            if (!context.HttpContext.User.Identity.IsAuthenticated && !await _unitOfWork.TokenRepository.IsCurrentActiveToken())
            {
                context.HttpContext.Response.Cookies.Delete("Authorization");
                CookieBuilder cookie = CreateAuthorizationCookie(-60);
                context.HttpContext.Response.Cookies.Append("Authorization", "", cookie.Build(context.HttpContext));
                context.HttpContext.Response.Redirect("/User/Login");
            }

        }
    }
}

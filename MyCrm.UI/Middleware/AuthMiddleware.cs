using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;
using MyCrm.Domain.Repositories;

namespace MyCrm.UI.Middleware
{
    public class AuthMiddleware : IMiddleware
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthMiddleware(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
            if (!context.User.Identity.IsAuthenticated)
            {
                var endpoint = context
                    .GetEndpoint();

                if (endpoint != null)
                {
                    var controllerActionDescriptor = endpoint
                        .Metadata
                        .GetMetadata<ControllerActionDescriptor>();

                    if (controllerActionDescriptor != null)
                    {
                        var actionName = controllerActionDescriptor.ActionName;

                        if (!actionName.Contains("Login")
                            && !actionName.Contains("Index")
                            && !actionName.Contains("Logout")
                            && !actionName.Contains("Register")
                            && !actionName.Contains("Logout")
                            && !await _unitOfWork.TokenRepository.IsCurrentActiveToken())
                        {
                            context.Response.Redirect("/User/Login");
                        }
                    }
                }
            }
        }
    }
}

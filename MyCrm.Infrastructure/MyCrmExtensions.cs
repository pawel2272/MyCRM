using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyCrm.Domain;
using MyCrm.Domain.Query.Dto.Auth;
using MyCrm.Domain.Repositories;
using MyCrm.Infrastructure.Repositories;

namespace MyCrm.Infrastructure
{
    public static class MyCrmExtensions
    {
        public static void PopulateValidation(this ModelStateDictionary modelState, IEnumerable<Result.Error> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.Message);
            }
        }
    }
}

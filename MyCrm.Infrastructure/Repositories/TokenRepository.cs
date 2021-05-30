using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using MyCrm.Domain.Query.Dto.Auth;
using MyCrm.Domain.Repositories;

namespace MyCrm.Infrastructure.Repositories
{
    public sealed class TokenRepository : ITokenRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtOptions _jwtOptions;
        private readonly IDistributedCache _cache;

        public TokenRepository(IHttpContextAccessor httpContextAccessor, JwtOptions jwtOptions, IDistributedCache cache)
        {
            _cache = cache;
            _jwtOptions = jwtOptions;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> IsCurrentActiveToken()
        {
            return await IsActiveAsync(GetCurrentAsync());
        }

        public async Task DeactivateCurrentAsync()
        {
            await DeactivateAsync(GetCurrentAsync());
        }

        public async Task<bool> IsActiveAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            return await _cache.GetStringAsync(GetKey(token)) == null;
        }

        public async Task DeactivateAsync(string token)
        {
            await _cache.SetStringAsync(GetKey(token),
                "deactivated", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(_jwtOptions.JwtExpireMinutes)
                });
        }

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Cookies["Authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Split(" ").Last();
        }

        private static string GetKey(string token)
        {
            return $"tokens:{token}:deactivated";
        }
    }
}

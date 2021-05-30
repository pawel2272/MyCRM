using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Dto.Auth;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Repositories;

namespace MyCrm.Infrastructure.Repositories
{
    public sealed class UsersRepository : IUsersRepository
    {
        private readonly CRMContext _dbContext;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenRepository _tokenRepository;

        public UsersRepository(CRMContext dbContext, IPasswordHasher<User> hasher, JwtOptions jwtOptions, IHttpContextAccessor contextAccessor, ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _contextAccessor = contextAccessor;
            _jwtOptions = jwtOptions;
            _hasher = hasher;
            _dbContext = dbContext;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public async Task<UserPageResult<User>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _dbContext.Users
                .Where(u => searchPhrase == null ||
                             (u.Id.ToString().Contains(searchPhrase)
                              || u.Username.ToLower().Contains(searchPhrase.ToLower())
                              || u.Gender.ToLower().Contains(searchPhrase.ToLower())
                              || u.FirstName.ToLower().Contains(searchPhrase.ToLower())
                              || u.LastName.ToLower().Contains(searchPhrase.ToLower())
                              || u.Phone.ToLower().Contains(searchPhrase.ToLower())
                              || u.Email.ToLower().Contains(searchPhrase.ToLower())
                              || u.Street.ToLower().Contains(searchPhrase.ToLower())
                              || u.PostalCode.ToLower().Contains(searchPhrase.ToLower())
                              || u.City.ToLower().Contains(searchPhrase.ToLower())
                             ));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<User, object>>>()
                {
                    { nameof(User.Username), u => u.Username },
                    { nameof(User.FirstName), u => u.FirstName },
                    { nameof(User.LastName), u => u.LastName },
                    { nameof(User.Phone), u => u.Phone },
                    { nameof(User.Email), u => u.Email },
                    { nameof(User.Street), u => u.Street },
                    { nameof(User.PostalCode), u => u.PostalCode },
                    { nameof(User.City), u => u.City }
                };

                Expression<Func<User, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["Username"];
                }

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var users = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return new UserPageResult<User>(users, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
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

        private JsonWebToken CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtKey));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddMinutes(_jwtOptions.JwtExpireMinutes);
            JwtSecurityToken token = new JwtSecurityToken(_jwtOptions.JwtIssuer,
                _jwtOptions.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.ToUniversalTime().Ticks - centuryBegin.Ticks).TotalSeconds);
            return new JsonWebToken()
            {
                AccessToken = tokenString,
                Expires = exp
            };
        }

        public async Task<string> LoginAsync(string username, string password, bool rememberMe)
        {
            var userToBeVerified = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            if (userToBeVerified == null)
            {
                return null;
            }
            var result = _hasher.VerifyHashedPassword(userToBeVerified, userToBeVerified.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var cookie = rememberMe ? CreateAuthorizationCookie(_jwtOptions.JwtExpireMinutes * 10) : CreateAuthorizationCookie(_jwtOptions.JwtExpireMinutes);
            var token = CreateToken(userToBeVerified);
            _contextAccessor.HttpContext.Response.Cookies.Append("Authorization", token.AccessToken, cookie.Build(_contextAccessor.HttpContext));
            return token.AccessToken;
        }

        public async Task LogoutAsync()
        {
            await _tokenRepository.DeactivateCurrentAsync();
            _contextAccessor.HttpContext.Response.Cookies.Delete("Authorization");
            CookieBuilder cookie = CreateAuthorizationCookie(-60);
            _contextAccessor.HttpContext.Response.Cookies.Append("Authorization", "", cookie.Build(_contextAccessor.HttpContext));
        }
    }
}

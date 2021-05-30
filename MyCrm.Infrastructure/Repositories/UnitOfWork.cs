using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Query.Dto.Auth;
using MyCrm.Domain.Repositories;

namespace MyCrm.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly CRMContext _context;

        public UnitOfWork(CRMContext context, IPasswordHasher<User> hasher, JwtOptions jwtOptions, IHttpContextAccessor contextAccessor, IDistributedCache distributedCache)
        {
            _context = context;
            ContactsRepository = new ContactsRepository(context);
            OrdersRepository = new OrdersRepository(context);
            RolesRepository = new RolesRepository(context);
            TodosRepository = new TodosRepository(context);
            TokenRepository = new TokenRepository(contextAccessor, jwtOptions, distributedCache);
            UsersRepository = new UsersRepository(context, hasher, jwtOptions, contextAccessor, TokenRepository);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IContactsRepository ContactsRepository { get; }
        public IOrdersRepository OrdersRepository { get; }
        public IRolesRepository RolesRepository { get; }
        public ITodosRepository TodosRepository { get; }
        public IUsersRepository UsersRepository { get; }
        public ITokenRepository TokenRepository { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using System.Threading.Tasks;

namespace MyCrm.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}

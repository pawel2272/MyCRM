using System.Threading.Tasks;

namespace MyCrm.Domain
{
    public interface IDependencyResolver
    {
        T ResolveOrDefault<T>() where T : class;
    }
}

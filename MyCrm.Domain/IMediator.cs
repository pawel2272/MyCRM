using System.Threading.Tasks;
using MyCrm.Domain.Command;
using MyCrm.Domain.Query;

namespace MyCrm.Domain
{
    public interface IMediator
    {
        Task<Result> CommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query);
        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}

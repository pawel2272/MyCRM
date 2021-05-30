using System;
using System.Linq;
using System.Threading.Tasks;
using MyCrm.Domain;
using MyCrm.Domain.Command;
using MyCrm.Domain.Query;

namespace MyCrm.Infrastructure
{
    public sealed class Mediator : IMediator
    {
        private readonly IDependencyResolver _dependencyResolver;

        public Mediator(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task<Result> CommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _dependencyResolver.ResolveOrDefault<ICommandHandler<TCommand>>();

            if (handler == null)
            {
                throw new InvalidOperationException($"Command of type '{command.GetType()}' has not registered handler.");
            }

            return await handler.HandleAsync(command);
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query)
        {
            return (TResponse)GetType()
                .GetMethods()
                .First(x => x.Name.Equals("QueryAsync") && x.GetGenericArguments().Length == 2)
                .MakeGenericMethod(query.GetType(), typeof(TResponse))
                .Invoke(this, new object[] {query});
        }

        public async Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var handler = _dependencyResolver.ResolveOrDefault<IQueryHandler<TQuery, TResponse>>();

            if (handler == null)
            {
                throw new InvalidOperationException($"Query of type '{query.GetType()}' has not registered handler.");
            }

            return await handler.HandleAsync(query);
        }
    }
}

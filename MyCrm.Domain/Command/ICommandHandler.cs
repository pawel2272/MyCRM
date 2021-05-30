using System.Threading.Tasks;

namespace MyCrm.Domain.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<Result> HandleAsync(TCommand command);
    }
}

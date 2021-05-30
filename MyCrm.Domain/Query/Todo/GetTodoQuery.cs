using MyCrm.Domain.Query.Dto;
using System;

namespace MyCrm.Domain.Query.Todo
{
    public sealed class GetTodoQuery : IQuery<TodoDto>
    {
        public GetTodoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

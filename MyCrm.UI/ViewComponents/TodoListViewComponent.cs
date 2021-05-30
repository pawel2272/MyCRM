using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyCrm.Domain;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Query.Todo;

namespace MyCrm.UI.ViewComponents
{
    public class TodoListViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public TodoListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(SearchTodosQuery query)
        {
            var items = await GetItemsAsync(query);
            return View(items);
        }

        private async Task<TodoPageResult<TodoDto>> GetItemsAsync(SearchTodosQuery query)
        {
            var todos = await _mediator.QueryAsync(query);
            return todos;
        }
    }
}
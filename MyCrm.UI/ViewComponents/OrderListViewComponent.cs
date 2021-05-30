using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyCrm.Domain;
using MyCrm.Domain.Query.Dto;
using MyCrm.Domain.Query.Dto.Pagination.PageResults;
using MyCrm.Domain.Query.Order;

namespace MyCrm.UI.ViewComponents
{
    public class OrderListViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(SearchOrdersQuery query)
        {
            var items = await GetItemsAsync(query);
            return View(items);
        }

        private async Task<OrderPageResult<OrderDto>> GetItemsAsync(SearchOrdersQuery query)
        {
            var orders = await _mediator.QueryAsync(query);
            return orders;
        }
    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Command.Order;
using MyCrm.Domain.Query.Order;
using MyCrm.Infrastructure;
using MyCrm.UI.Filters;

namespace MyCrm.UI.Controllers
{
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public ActionResult Add(Guid contactId)
        {
            var command = new AddOrderCommand()
            {
                ContactId = contactId
            };
            return View(command);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddOrderCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("Show", "Contact", new { id = command.ContactId });
        }

        public async Task<ActionResult> Delete(DeleteOrderCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (!result.IsSuccess)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("Show", "Contact", new { id = command.ContactId });
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var order = await _mediator.QueryAsync(new GetOrderQuery(id));
            var model = _mapper.Map<EditOrderCommand>(order);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditOrderCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (!result.IsSuccess)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("Show", "Contact", new { id = command.ContactId });
        }
    }
}
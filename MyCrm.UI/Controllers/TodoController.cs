using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain;
using MyCrm.Domain.Command.Order;
using MyCrm.Domain.Command.Todo;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Contact;
using MyCrm.Domain.Query.Todo;
using MyCrm.Infrastructure;
using MyCrm.UI.Filters;

namespace MyCrm.UI.Controllers
{
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TodoController(ILogger<TodoController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public ActionResult Add(Guid contactId)
        {
            var command = new AddTodoCommand()
            {
                ContactId = contactId
            };
            return View(command);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddTodoCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("Show", "Contact", new { id = command.ContactId });
        }

        public async Task<ActionResult> Delete(DeleteTodoCommand command)
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
            var result = await _mediator.QueryAsync(new GetTodoQuery(id));
            var model = _mapper.Map<EditTodoCommand>(result);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTodoCommand command)
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
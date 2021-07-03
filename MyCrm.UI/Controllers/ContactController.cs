using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Query.Contact;
using MyCrm.Infrastructure;
using MyCrm.UI.Filters;

namespace MyCrm.UI.Controllers
{
    [ServiceFilter(typeof(JwtAuthFilter))]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContactController(ILogger<ContactController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<ActionResult> Show(Guid id)
        {
            var contact = await _mediator.QueryAsync(new GetContactQuery(id));
            ViewData["contactId"] = id;
            return View(contact);
        }

        public async Task<ActionResult> List(SearchContactsQuery query)
        {
            if (query.UserId.Equals(Guid.Empty))
            {
                var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                query.UserId = userId;
            }

            ViewBag.PageNumber = query.PageNumber;

            var contacts = await _mediator.QueryAsync(query);
            return View(contacts);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddContactCommand command)
        {
            if (command.UserId.Equals(Guid.Empty))
            {
                var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                command.UserId = userId;
            }
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("List", new { PageNumber = 1, PageSize = 10, OrderBy = "FirstName"});
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteContactCommand(id));
            if (!result.IsSuccess)
            {
                ModelState.PopulateValidation(result.Errors);
            }

            return RedirectToAction("List", new { PageNumber = 1, PageSize = 10, OrderBy = "FirstName"});
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var contact = await _mediator.QueryAsync(new GetContactQuery(id));
            var model = _mapper.Map<EditContactCommand>(contact);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditContactCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (!result.IsSuccess)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("List", new { PageNumber = 1, PageSize = 10, OrderBy = "FirstName"});
        }
    }
}
﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Query.Contact;
using MyCrm.Domain.Repositories;
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

        public async Task<ActionResult> Show(GetContactQuery query)
        {
            return View(query.Id);
        }

        public async Task<ActionResult> List(SearchContactsQuery query)
        {
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
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                return View(command);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteContactCommand(id));
            if (!result.IsSuccess)
            {
                ViewData["Error"] = result.Message;
            }

            return RedirectToAction("List");
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
                return View(command);
            }

            return RedirectToAction("List");
        }
    }
}
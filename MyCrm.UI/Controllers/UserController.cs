﻿using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using MyCrm.Domain;
using MyCrm.Domain.Command.User;
using MyCrm.Domain.Enums;
using MyCrm.Domain.Query.Role;
using MyCrm.Domain.Query.User;
using MyCrm.Infrastructure;
using MyCrm.UI.Filters;

namespace MyCrm.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Info");
        }

        [ServiceFilter(typeof(JwtAuthFilter))]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.CommandAsync(new LogoutCommand());

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(AddUserCommand command)
        {
            var role = await _mediator.QueryAsync(new SearchRolesQuery()
            {
                SearchPhrase = "Admin",
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "Name",
                SortDirection = SortDirection.DESC
            });

            command.RoleId = role.Items.FirstOrDefault(r => r != null).Id;

            var result = await _mediator.CommandAsync(command);

            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View();
            }

            return RedirectToAction("Login");
        }

        [ServiceFilter(typeof(JwtAuthFilter))]
        [Authorize]
        [ResponseCache(Duration = 1200, Location = ResponseCacheLocation.Client)]
        public async Task<ActionResult> Info()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = await _mediator.QueryAsync(new GetUserQuery(userId));
            return View(user);
        }

        [ServiceFilter(typeof(JwtAuthFilter))]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = await _mediator.QueryAsync(new GetUserQuery(userId));
            var command = _mapper.Map<EditUserCommand>(user);
            return View(command);
        }

        [ServiceFilter(typeof(JwtAuthFilter))]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(EditUserCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsFailure)
            {
                ModelState.PopulateValidation(result.Errors);
                return View(command);
            }

            return RedirectToAction("Info", "User");
        }
    }
}
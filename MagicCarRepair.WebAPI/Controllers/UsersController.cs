using MagicCarRepair.Application.Features.Users.Commands.CreateUser;
using MagicCarRepair.Application.Features.Users.Commands.DeleteUser;
using MagicCarRepair.Application.Features.Users.Commands.UpdateUser;
using MagicCarRepair.Application.Features.Users.Queries.GetUserById;
using MagicCarRepair.Application.Features.Users.Queries.GetUsers;
using MagicCarRepair.Application.Features.Users.Commands.AssignRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepair.WebAPI.Controllers;

[Authorize]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetUsersQuery request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new GetUserByIdQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserCommand request)
    {
        request.Id = id;
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new DeleteUserCommand { Id = id });
        return Ok(response);
    }

    [HttpPost("{id}/roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRoles([FromRoute] Guid id, [FromBody] AssignUserRolesCommand request)
    {
        request.UserId = id;
        var response = await Mediator.Send(request);
        return Ok(response);
    }
} 
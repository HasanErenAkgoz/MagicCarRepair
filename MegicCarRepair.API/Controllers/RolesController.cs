using MagicCarRepair.Application.Features.Roles.Commands.CreateRole;
using MagicCarRepair.Application.Features.Roles.Commands.UpdateRole;
using MediatR;
using MegicCarRepairAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepair.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
public class RolesController : BaseApiController
{

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRoleCommand request)
    {
        request.Id = id;
        var response = await Mediator.Send(request);
        return Ok(response);
    }
} 
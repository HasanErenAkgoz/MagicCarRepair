using MagicCarRepair.Application.Features.Services.Commands.CreateService;
using MagicCarRepair.Application.Features.Services.Commands.DeleteService;
using MagicCarRepair.Application.Features.Services.Commands.UpdateService;
using MagicCarRepair.Application.Features.Services.Queries.GetServiceById;
using MagicCarRepair.Application.Features.Services.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepair.WebAPI.Controllers;

[Authorize]
public class ServicesController : BaseController
{
    public ServicesController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetServicesQuery request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new GetServiceByIdQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCommand request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceCommand request)
    {
        request.Id = id;
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new DeleteServiceCommand { Id = id });
        return Ok(response);
    }
} 
using MagicCarRepair.Application.Features.Vehicles.Commands.CreateVehicle;
using MagicCarRepair.Application.Features.Vehicles.Commands.DeleteVehicle;
using MagicCarRepair.Application.Features.Vehicles.Commands.UpdateVehicle;
using MagicCarRepair.Application.Features.Vehicles.Queries.GetVehicleById;
using MagicCarRepair.Application.Features.Vehicles.Queries.GetVehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepair.WebAPI.Controllers;

[Authorize]
public class VehiclesController : BaseController
{
    public VehiclesController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetVehiclesQuery request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new GetVehicleByIdQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleCommand request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateVehicleCommand request)
    {
        request.Id = id;
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new DeleteVehicleCommand { Id = id });
        return Ok(response);
    }
} 
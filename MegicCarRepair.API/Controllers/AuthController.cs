using MagicCarRepair.Application.Features.Auth.Commands.Login;
using MagicCarRepair.Application.Features.Auth.Commands.Register;
using MagicCarRepair.Application.Features.Auth.Commands.ResetPassword;
using MediatR;
using MegicCarRepairAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepair.WebAPI.Controllers;

public class AuthController : BaseApiController
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
} 
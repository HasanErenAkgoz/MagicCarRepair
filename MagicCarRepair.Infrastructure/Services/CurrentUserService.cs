using System.Security.Claims;
using MagicCarRepair.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepair.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? Guid.Parse(userId) : Guid.Empty;
        }
    }


    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> Roles => 
        _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value) ?? Array.Empty<string>();

    public IEnumerable<string> Permissions =>
        _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value) ?? Array.Empty<string>();
} 
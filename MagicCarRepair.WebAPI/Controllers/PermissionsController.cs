using MagicCarRepair.Application.Features.Permissions.Queries.GetGroupedPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Yetki işlemleri için API endpoint'leri
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PermissionsController : BaseController
{
    /// <summary>
    /// Sistemdeki tüm yetkileri listeler
    /// </summary>
    /// <returns>Yetki listesi</returns>
    /// <response code="200">Yetkiler başarıyla listelendi</response>
    /// <response code="401">Yetkisiz erişim</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetList()
    {
        var query = new GetPermissionListQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Rol yetkilerini günceller
    /// </summary>
    /// <param name="command">Güncellenecek rol ve yetkiler</param>
    /// <returns>İşlem sonucu</returns>
    /// <response code="200">Yetkiler başarıyla güncellendi</response>
    /// <response code="400">Geçersiz veri</response>
    /// <response code="401">Yetkisiz erişim</response>
    /// <response code="404">Rol bulunamadı</response>
    [HttpPut("roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRolePermissions([FromBody] UpdateRolePermissionsCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
} 
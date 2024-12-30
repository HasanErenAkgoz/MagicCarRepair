using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MagicCarRepair.Application.Features.Cars.Commands;
using MagicCarRepair.Application.Features.Cars.Queries;
using MediatR;
using System;
using System.Threading.Tasks;

namespace MagicCarRepair.WebAPI.Controllers
{
    /// <summary>
    /// Araç işlemleri için API endpoint'leri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : BaseController
    {
        /// <summary>
        /// Yeni araç kaydı oluşturur
        /// </summary>
        /// <param name="command">Araç bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        /// <response code="200">Araç başarıyla kaydedildi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="401">Yetkisiz erişim</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// ID'ye göre araç bilgilerini getirir
        /// </summary>
        /// <param name="id">Araç ID</param>
        /// <returns>Araç detayları</returns>
        /// <response code="200">Araç bilgileri başarıyla getirildi</response>
        /// <response code="404">Araç bulunamadı</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetCarByIdQuery { Id = id });
            return Ok(result);
        }

        // Diğer endpoint'ler...
    }
} 
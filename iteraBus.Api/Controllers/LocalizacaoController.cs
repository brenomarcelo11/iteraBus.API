using iteraBus.Api.Models;
using iteraBus.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalizacaoController : ControllerBase
    {
        private readonly ILocalizacaoAplicacao _localizacaoAplicacao;

        public LocalizacaoController(ILocalizacaoAplicacao localizacaoAplicacao)
        {
            _localizacaoAplicacao = localizacaoAplicacao;
        }

        [HttpGet]
        [Route("Obter/{localizacaoId}")]
        public async Task<IActionResult> ObterLocalizacaoPorId(int localizacaoId)
        {
            try
            {
                var localizacaoDominio = await _localizacaoAplicacao.ObterLocalizacaoPorIdAsync(localizacaoId);
                var localizacaoResponse = new LocalizacaoResponse()
                {
                    Id = localizacaoDominio.Id,
                    OnibusId = localizacaoDominio.OnibusId,
                    Latitude = localizacaoDominio.Latitude,
                    Longitude = localizacaoDominio.Longitude
                };

                return Ok(localizacaoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
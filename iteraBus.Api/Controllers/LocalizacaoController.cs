using iteraBus.Api.Models;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
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

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarLocalizacaoAsync([FromBody] LocalizacaoCriar localizacaoCriar)
        {
            try
            {
                var localizacaoDominio = new Localizacao()
                {
                    OnibusId = localizacaoCriar.OnibusId,
                    Latitude = localizacaoCriar.Latitude,
                    Longitude = localizacaoCriar.Longitude
                };

                var localizacaoId = await _localizacaoAplicacao.AdicionarLocalizacaoAsync(localizacaoDominio);
                return Ok(localizacaoId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> EditarLocalizacaoAsync(LocalizacaoAtualizar localizacaoAtualizar)
        {
            try 
            {
                var localizacaoDominio = new Localizacao()
                {
                    Id = localizacaoAtualizar.Id,
                    Latitude = localizacaoAtualizar.Latitude,
                    Longitude = localizacaoAtualizar.Longitude,
                    OnibusId = localizacaoAtualizar.OnibusId
                };

                await _localizacaoAplicacao.EditarLocalizacaoAsync(localizacaoDominio);

                return Ok(localizacaoDominio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarLocalizacaoAsync()
        {
            try
            {
                var localizacaoDominio = await _localizacaoAplicacao.ListarLocalizacaoAsync();
                var localizacoes = localizacaoDominio.Select(localizacao => new LocalizacaoResponse()
                {
                    Id = localizacao.Id,
                    Latitude = localizacao.Latitude,
                    Longitude = localizacao.Longitude,
                    OnibusId = localizacao.OnibusId
                }).ToList();

                return Ok(localizacoes);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using iteraBus.Api.Models;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PontoDeOnibusController : ControllerBase
    {
        private readonly IPontoDeOnibusAplicacao _pontoDeOnibusAplicacao;

        public PontoDeOnibusController(IPontoDeOnibusAplicacao pontoDeOnibusAplicacao)
        {
            _pontoDeOnibusAplicacao = pontoDeOnibusAplicacao;
        }

        [HttpGet]
        [Route("Obter/{pontoId}")]
        public async Task<IActionResult> ObterPontoPorId(int pontoId)
        {
            try
            {
                var pontoDeOnibusDominio = await _pontoDeOnibusAplicacao.ObterPontoPorIdAsync(pontoId);
                var pontoDeOnibusResponse = new PontoDeOnibusResponse()
                {
                    Id = pontoDeOnibusDominio.Id,
                    Nome = pontoDeOnibusDominio.Nome,
                    Latitude = pontoDeOnibusDominio.Latitude,
                    Longitude = pontoDeOnibusDominio.Longitude
                };

                return Ok(pontoDeOnibusResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarPontoAsync([FromBody] PontoDeOnibusCriar pontoDeOnibusCriar)
        {
            try
            {
                var pontoDeOnibusDominio = new PontoDeOnibus()
                {
                    Nome = pontoDeOnibusCriar.Nome,
                    Latitude = pontoDeOnibusCriar.Latitude,
                    Longitude = pontoDeOnibusCriar.Longitude
                };

                var pontoId = await _pontoDeOnibusAplicacao.AdicionarPontoAsync(pontoDeOnibusDominio);
                return Ok(pontoId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> EditarPontoAsync(PontoDeOnibusAtualizar pontoDeOnibusAtualizar)
        {
            try
            {
                var pontoDeOnibusDominio = new PontoDeOnibus()
                {
                    Id = pontoDeOnibusAtualizar.Id,
                    Latitude = pontoDeOnibusAtualizar.Latitude,
                    Longitude = pontoDeOnibusAtualizar.Longitude,
                    Nome = pontoDeOnibusAtualizar.Nome
                };

                await _pontoDeOnibusAplicacao.EditarPontoAsync(pontoDeOnibusDominio);

                return Ok(pontoDeOnibusDominio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarPontosAsync()
        {
            try
            {
                var pontoDeOnibusDominio = await _pontoDeOnibusAplicacao.ListarPontosAsync();
                var pontos = pontoDeOnibusDominio.Select(ponto => new PontoDeOnibusResponse()
                {
                    Id = ponto.Id,
                    Latitude = ponto.Latitude,
                    Longitude = ponto.Longitude,
                    Nome = ponto.Nome
                }).ToList();

                return Ok(pontos);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{pontoId}")]
        public async Task<IActionResult> ExcluirPontoAsync(int pontoId)
        {
            try
            {
                await _pontoDeOnibusAplicacao.ExcluirPontoAsync(pontoId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
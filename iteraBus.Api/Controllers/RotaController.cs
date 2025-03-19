using iteraBus.Api.Models;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly IRotaAplicacao _rotaAplicacao;
        public RotaController(IRotaAplicacao rotaAplicacao)
        {
            _rotaAplicacao = rotaAplicacao;
        }


        [HttpGet]
        [Route("Obter/{rotaId}")]
        public async Task<IActionResult> ObterRotaPorIdAsync([FromRoute] int rotaId)
        {
            try
            {
                var rotaDominio = await _rotaAplicacao.ObterRotaPorIdAsync(rotaId);
                var rotaResponse = new RotaResponse()
                {
                    Id = rotaDominio.Id,
                    Nome = rotaDominio.Nome
                };

                return Ok(rotaResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarRotaAsync([FromBody] RotaCriar rotaCriar)
        {
            try
            {
                var rotaDominio = new Rota()
                {
                    Nome = rotaCriar.Nome
                };

                var rotaId = await _rotaAplicacao.AdicionarRotaAsync(rotaDominio);
                return Ok(rotaId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> EditarRotaAsync([FromBody] RotaAtualizar rotaAtualizar)
        {
            try
            {
                var rotaDominio = new Rota()
                {
                    Id = rotaAtualizar.Id,
                    Nome = rotaAtualizar.Nome
                };

                await _rotaAplicacao.EditarRotaAsync(rotaDominio);
                return Ok(rotaDominio);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarRotasAsync()
        {
            try
            {
                var rotaDominio = await _rotaAplicacao.ListarRotasAsync();
                var rotas = rotaDominio.Select(rotas => new RotaResponse()
                {
                    Id = rotas.Id,
                    Nome = rotas.Nome
                }).ToList();

                return Ok(rotas);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{rotaId}")]
        public async Task<IActionResult> ExcluirRotaAsync(int rotaId)
        {
            try
            {
                await _rotaAplicacao.ExcluirRotaAsync(rotaId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
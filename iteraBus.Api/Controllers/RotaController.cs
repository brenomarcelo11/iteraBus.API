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
                    Nome = rotaDominio.Nome,
                    OnibusIds = rotaDominio.Onibus?.Select(o => o.Id).ToList(),
                    PontosDeOnibus = rotaDominio.PontosDeOnibus?.Select(p => new PontoDeOnibusResponse
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    }).ToList()
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
                    Nome = rotaCriar.Nome,
                    Onibus = rotaCriar.OnibusIds?.Select(id => new Onibus { Id = id }).ToList(),
                    PontosDeOnibus = rotaCriar.PontosDeOnibusIds?.Select(id => new PontoDeOnibus{Id = id}).ToList()
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
                    Nome = rotaAtualizar.Nome,
                    Onibus = rotaAtualizar.OnibusIds?.Select(id => new Onibus { Id = id }).ToList(),
                    PontosDeOnibus = rotaAtualizar.PontosDeOnibusIds?.Select(id => new PontoDeOnibus { Id = id }).ToList()
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
                    Nome = rotas.Nome,
                    OnibusIds = rotas.Onibus?.Select(o => o.Id).ToList(),
                    PontosDeOnibus = rotas.PontosDeOnibus?.Select(p => new PontoDeOnibusResponse
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                        RotaId = p.RotaId
                    }).ToList()
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

        // [HttpGet]
        // [Route("ObterPontoPorRotaId{rotaId}")]
        // public async Task<IActionResult> ObterPontoDeOnibusPorRotaIdAsync(int rotaId)
        // {
        //     try 
        //     {
        //         var rotasDominio = await _rotaAplicacao.ObterPontoDeOnibusPorRotaIdAsync(rotaId);
        //         var rotas = rotasDominio.Select(rota => new RotaResponse()
        //         {
        //             Id = rota.Id,
        //             Nome = rota.Nome,
        //             PontosDeOnibus = rota.PontosDeOnibus,


        //         }
        //         )
        //     }

        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }
    }
}
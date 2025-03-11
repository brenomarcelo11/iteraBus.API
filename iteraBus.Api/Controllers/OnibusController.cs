using iteraBus.Api.Models;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnibusController : ControllerBase
    {
        private readonly IOnibusAplicacao _onibusAplicacao;

        public OnibusController(IOnibusAplicacao onibusAplicacao)
        {
            _onibusAplicacao = onibusAplicacao;
        }


        [HttpGet]
        [Route("Obter/{onibusId}")]
        public async Task<IActionResult> ObterOnibusPorIdAsync([FromRoute] int onibusId)
        {
            try
            {
                var onibusDominio = await _onibusAplicacao.ObterOnibusPorIdAsync(onibusId);
                var onibusResposta = new OnibusResponse()
                {
                    Id = onibusDominio.Id,
                    Placa = onibusDominio.Placa,
                    RotaId = onibusDominio.RotaId
                };

                return Ok(onibusResposta);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarOnibusAsync([FromBody] OnibusCriar onibusCriar)
        {
            try
            {
                var onibusDominio = new Onibus()
                {
                    Placa = onibusCriar.Placa,
                    RotaId = onibusCriar.RotaId
                };
                
                var onibusId = await _onibusAplicacao.AdicionarOnibusAsync(onibusDominio);
                return Ok(onibusId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> EditarOnibusAsync([FromBody] OnibusAtualizar onibusAtualizar)
        {   try 
            {
                var onibusDominio = new Onibus()
                {
                    Id = onibusAtualizar.Id,
                    Placa = onibusAtualizar.Placa,
                    RotaId = onibusAtualizar.RotaId
                };

                await _onibusAplicacao.EditarOnibusAsync(onibusDominio);

                return Ok(onibusDominio);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarOnibusAsync()
        {
            try
            {
                var onibusDominio = await _onibusAplicacao.ListarOnibusAsync();
                var onibus = onibusDominio.Select(onibus => new OnibusResponse()
                {
                    Id = onibus.Id,
                    Placa = onibus.Placa,
                    RotaId = onibus.RotaId
                }).ToList();

                return Ok(onibus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
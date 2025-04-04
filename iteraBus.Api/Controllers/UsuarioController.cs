using iteraBus.Api.Models;
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using iteraBus.Dominio.Enumeradores;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api
{
    [ApiController]
    [Route("[controller]")]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;

        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpGet]
        [Route("Obter/{usuarioId}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int usuarioId)
        {
            try
            {
                var usuarioDominio = await _usuarioAplicacao.ObterPorIdAsync(usuarioId);

                var usuarioResposta = new UsuarioResposta()
                {
                    Id = usuarioDominio.Id,
                    Nome = usuarioDominio.Nome,
                    Email = usuarioDominio.Email,
                    TipoUsuarioId = usuarioDominio.TipoUsuarioId
                };

                return Ok(usuarioResposta);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] UsuarioCriar usuarioCriar)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Nome = usuarioCriar.Nome,
                    Email = usuarioCriar.Email,
                    Senha = usuarioCriar.Senha,
                    TipoUsuarioId = usuarioCriar.TipoUsuarioId
                };

                var usuarioId = await _usuarioAplicacao.CriarAsync(usuarioDominio);

                return Ok(usuarioId);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] UsuarioAtualizar usuarioAtualizar)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Id = usuarioAtualizar.Id,
                    Nome = usuarioAtualizar.Nome,
                    Email = usuarioAtualizar.Email,
                    TipoUsuarioId = usuarioAtualizar.TipoUsuarioId
                };

                await _usuarioAplicacao.AtualizarAsync(usuarioDominio);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{usuarioId}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int usuarioId)
        {
            try
            {
                await _usuarioAplicacao.DeletarAsync(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("Restaurar/{usuarioId}")]
        public async Task<ActionResult> RestaurarAsync([FromRoute] int usuarioId)
        {
            try
            {
                await _usuarioAplicacao.RestaurarAsync(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> ListarAsync([FromQuery] bool ativos)
        {
            try
            {
                var usuariosDominio = await _usuarioAplicacao.ListarAsync(ativos);

                var usuarios = usuariosDominio.Select(usuario => new UsuarioResposta()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuarioId = usuario.TipoUsuarioId
                }).ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{usuarioId}/favoritar-rota/{rotaId}")]
        public async Task<IActionResult> FavoritarRota(int usuarioId, int rotaId)
        {
            try
            {
                await _usuarioAplicacao.FavoritarRotaAsync(usuarioId, rotaId);
                return Ok(new { mensagem = "Rota favoritada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{usuarioId}/desfavoritar-rota/{rotaId}")]
        public async Task<IActionResult> DesfavoritarRota(int usuarioId, int rotaId)
        {
            try
            {
                await _usuarioAplicacao.DesfavoritarRotaAsync(usuarioId, rotaId);
                return Ok(new { mensagem = "Rota desfavoritada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
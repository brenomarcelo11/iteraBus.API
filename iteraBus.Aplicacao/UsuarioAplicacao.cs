using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;

namespace Projeto360.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<int> CriarAsync(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Usuario nao pode ser vazio");
            }

            await ValidarInformacoesUsuarioAsync(usuario);
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("Senha nao pode ser vazio");
            }

            return await _usuarioRepositorio.CriarAsync(usuario);
        }


        public async Task AtualizarAsync(Usuario usuario)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao pode ser vazio");
            }

            await ValidarInformacoesUsuarioAsync(usuario);

            usuarioDominio.Nome = usuario.Nome;
            usuarioDominio.Email = usuario.Email;
            usuarioDominio.TipoUsuarioId = usuario.TipoUsuarioId;
            // alteracao


            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }

        public async Task AlterarSenhaAsync(Usuario usuario, string senhaAntiga)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao encontrado");
            }
            if (usuarioDominio.Senha != senhaAntiga)
            {
                throw new Exception("Senha antiga invalida");
            }

            usuarioDominio.Senha = usuario.Senha;

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }

        public async Task<Usuario> ObterPorIdAsync(int usuarioId)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao encontrado");
            }

            return usuarioDominio;
        }
        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorEmailAsync(email);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao encontrado");
            }

            return usuarioDominio;
        }
        public async Task<Usuario> ObterPorTokenAsync(string token)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorTokenAsync(token);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao encontrado");
            }

            return usuarioDominio;
        }
        public async Task DeletarAsync(int usuarioId)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao encontrado");
            }

            usuarioDominio.Deletar();

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);

        }
        public async Task RestaurarAsync(int usuarioId)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterDesativoAsync(usuarioId);

            if (usuarioDominio == null)
            {
                throw new Exception("Usuario nao encontrado");
            }

            usuarioDominio.Restaurar();

            await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
        }

        public async Task<IEnumerable<Usuario>> ListarAsync(bool ativo)
        {
            return await _usuarioRepositorio.ListarAsync(ativo);
        }

        public async Task FavoritarRotaAsync(int usuarioId, int rotaId)
        {
            var usuario = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            await _usuarioRepositorio.FavoritarRotaAsync(usuarioId, rotaId);
        }

        public async Task DesfavoritarRotaAsync(int usuarioId, int rotaId)
        {
            var usuario = await _usuarioRepositorio.ObterPorIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            await _usuarioRepositorio.DesfavoritarRotaAsync(usuarioId, rotaId);
        }




        #region Util
        private static async Task ValidarInformacoesUsuarioAsync(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
            {
                throw new Exception("Nome nao pode ser vazio");
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new Exception("Email nao pode ser vazio");
            }
        }
        #endregion
    }
}
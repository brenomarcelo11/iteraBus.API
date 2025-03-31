using iteraBus.Dominio.Entidades;

namespace iteraBus.Aplicacao.Interfaces
{
    public interface IUsuarioAplicacao
    {
        Task<int> CriarAsync(Usuario usuarioDTO);

        Task AlterarSenhaAsync(Usuario usuarioDTO, string novaSenha);

        Task AtualizarAsync(Usuario usuarioDTO);

        Task DeletarAsync(int usuarioId);

        Task RestaurarAsync(int usuarioId);

        Task<Usuario> ObterPorIdAsync(int usuarioId);
        
        Task<Usuario> ObterPorEmailAsync(string email);
        Task<Usuario> ObterPorTokenAsync(string token);

        Task<IEnumerable<Usuario>> ListarAsync(bool ativo);
    }
}
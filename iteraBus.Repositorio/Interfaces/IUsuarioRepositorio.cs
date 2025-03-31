using iteraBus.Dominio.Entidades;

public interface IUsuarioRepositorio
{
    Task<int> CriarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task<Usuario> ObterPorIdAsync(int usuarioId);
    Task<Usuario> ObterDesativoAsync(int usuarioId);
    Task<Usuario> ObterPorEmailAsync(string email);
    Task<Usuario> ObterPorTokenAsync(string token);
    Task<IEnumerable<Usuario>> ListarAsync(bool ativo);
}
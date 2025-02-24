using iteraBus.Dominio.Entidades;

namespace iteraBus.Repositorio.Inteface 
{
    public interface ILocalizacaoRepositorio
    {
        Task<int> AdicionarLocalizacaoAsync(Localizacao lcalizacao);
        Task EditarLocalizacaoAsync(Localizacao localizacao);
        Task<IEnumerable<Localizacao>> ListarLocalizacoesAsync();
        Task<Localizacao> ObterLocalizacaoPorIdAsync(int localizacaoId);

    }
}
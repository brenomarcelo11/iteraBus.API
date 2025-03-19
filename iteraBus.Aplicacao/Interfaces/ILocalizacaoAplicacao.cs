using iteraBus.Dominio.Entidades;

namespace iteraBus.Aplicacao.Interfaces
{
    public interface ILocalizacaoAplicacao
    {
        Task<int> AdicionarLocalizacaoAsync(Localizacao localizacao);
        Task EditarLocalizacaoAsync(Localizacao localizacao);
        Task<IEnumerable<Localizacao>> ListarLocalizacaoAsync();
        Task<Localizacao> ObterLocalizacaoPorIdAsync(int localizacaoId);
        Task ExcluirLocalizacaoAsync(int localizacaoId);
    }
}
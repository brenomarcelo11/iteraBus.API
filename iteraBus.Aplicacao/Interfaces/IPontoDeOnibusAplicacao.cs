using iteraBus.Dominio.Entidades;

namespace iteraBus.Aplicacao.Interfaces
{
    public interface IPontoDeOnibusAplicacao
    {
        Task<int> AdicionarPontoAsync(PontoDeOnibus ponto);
        Task EditarPontoAsync(PontoDeOnibus ponto);
        Task<IEnumerable<PontoDeOnibus>> ListarPontosAsync();
        Task<PontoDeOnibus> ObterPontoPorIdAsync(int pontoId);
        Task ExcluirPontoAsync(int pontoId);
        
    }
}
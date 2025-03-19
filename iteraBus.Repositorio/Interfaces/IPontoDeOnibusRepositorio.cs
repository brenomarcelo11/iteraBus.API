using iteraBus.Dominio.Entidades;

namespace iteraBus.Dominio.Repositorios
{
    public interface IPontoDeOnibusRepositorio
    {
        Task<List<PontoDeOnibus>> ListarPontosAsync();
        Task<PontoDeOnibus> ObterPontoPorIdAsync(int id);
        Task<int> AdicionarPontoAsync(PontoDeOnibus ponto);
        Task AtualizarPontoAsync(PontoDeOnibus ponto);
        Task ExcluirPontoAsync(int id);
    }
}
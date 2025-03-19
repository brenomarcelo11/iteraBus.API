using iteraBus.Dominio.Entidades;

namespace iteraBus.Repositorio.Inteface 
{
    public interface IRotaRepositorio
    {
        Task<int> AdicionarRotaAsync(Rota rota);
        Task EditarRotaAsync(Rota rota);
        Task<IEnumerable<Rota>> ListarRotasAsync();
        Task<Rota> ObterRotaPorIdAsync(int rotaId);
        Task ExcluirRotaAsync(int rotaId);

    }
}
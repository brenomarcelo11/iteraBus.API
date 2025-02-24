using iteraBus.Dominio.Entidades;

namespace iteraBus.Aplicacao.Interfaces
{
    public interface IRotaAplicacao
    {
        Task<int> AdicionarRotaAsync(Rota rota);
        Task EditarRotaAsync(Rota rota);
        Task<IEnumerable<Rota>> ListarRotasAsync();
        Task<Rota> ObterRotaPorIdAsync(int rotaId);
        
    }
}
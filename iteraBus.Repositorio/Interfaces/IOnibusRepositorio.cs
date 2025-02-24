using iteraBus.Dominio.Entidades;

namespace iteraBus.Repositorio.Inteface 
{
    public interface IOnibusRepositorio
    {
        Task<int> AdicionarOnibusAsync(Onibus onibus);
        Task EditarOnibusAsync(Onibus onibus);
        Task<IEnumerable<Onibus>> ListarOnibusAsync();
        Task<Onibus> ObterOnibusPorIdAsync(int onibusId);

    }
}
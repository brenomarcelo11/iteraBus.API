using iteraBus.Dominio.Entidades;

namespace iteraBus.Aplicacao.Interfaces
{
    public interface IOnibusAplicacao
    {
        Task<int> AdicionarOnibusAsync(Onibus onibus);
        Task EditarOnibusAsync(Onibus onibus);
        Task<IEnumerable<Onibus>> ListarOnibusAsync();
        Task<Onibus> ObterOnibusPorIdAsync(int onibusId);
        Task ExcluirOnibusAsync(int onibusId);

    }
}
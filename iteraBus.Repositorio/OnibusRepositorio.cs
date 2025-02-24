using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;
using iteraBus.Repositorio.Inteface;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio
{
    public class OnibusRepositorio : BaseRepositorio, IOnibusRepositorio
    {
        public OnibusRepositorio(IteraBusContexto contexto) : base(contexto)
        {}

        public async Task<int> AdicionarOnibusAsync(Onibus onibus)
        {
            _contexto.Onibus.Add(onibus);
            await _contexto.SaveChangesAsync();
            return onibus.Id;
        }

        public async Task EditarOnibusAsync(Onibus onibus)
        {
            _contexto.Onibus.Update(onibus);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Onibus>> ListarOnibusAsync()
        {
            return await _contexto.Onibus.ToListAsync();
        }

        public async Task<Onibus> ObterOnibusPorIdAsync(int onibusId)
        {
            return await _contexto.Onibus
                        .Where(o => o.Id == onibusId)
                        .FirstOrDefaultAsync();
        }
    }
}
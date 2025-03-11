using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;
using iteraBus.Repositorio.Inteface;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio
{
    public class RotaRepositorio : BaseRepositorio, IRotaRepositorio
    {
        public RotaRepositorio(IteraBusContexto contexto) : base(contexto)
        {
        }

        public async Task<int> AdicionarRotaAsync(Rota rota)
        {
            _contexto.Rotas.Add(rota);
            await _contexto.SaveChangesAsync();
            return rota.Id;
        }

        public async Task EditarRotaAsync(Rota rota)
        {
            _contexto.Rotas.Update(rota);

            foreach (var onibus in rota.Onibus)
            {
                _contexto.Onibus.Update(onibus); 
            }
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rota>> ListarRotasAsync()
        {
            return await _contexto.Rotas.ToListAsync();
        }

        public async Task<Rota> ObterRotaPorIdAsync(int rotaId)
        {
            return await _contexto.Rotas
                .Where(r => r.Id == rotaId)
                .FirstOrDefaultAsync();
        }

        public async Task ExcluirRotaAsync(int rotaId)
        {
            var rotaExcluida = await _contexto.Rotas.FindAsync(rotaId);
            _contexto.Rotas.Remove(rotaExcluida);
            await _contexto.SaveChangesAsync();
        }

    }
}
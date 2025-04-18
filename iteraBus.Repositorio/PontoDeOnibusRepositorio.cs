using iteraBus.Dominio.Entidades;
using iteraBus.Dominio.Repositorios;
using iteraBus.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio
{
    public class PontoDeOnibusRepositorio : BaseRepositorio, IPontoDeOnibusRepositorio
    {
        public PontoDeOnibusRepositorio(IteraBusContexto contexto) : base(contexto)
        { }
        public async Task<int> AdicionarPontoAsync(PontoDeOnibus ponto)
        {
            _contexto.PontosDeOnibus.Add(ponto);
            await _contexto.SaveChangesAsync();
            return ponto.Id;
        }

        public async Task AtualizarPontoAsync(PontoDeOnibus ponto)
        {
            _contexto.PontosDeOnibus.Update(ponto);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirPontoAsync(int id)
        {
           var pontoExcluido = await _contexto.PontosDeOnibus.FindAsync(id);
            _contexto.PontosDeOnibus.Remove(pontoExcluido);
            await _contexto.SaveChangesAsync();
        }

        public async Task<List<PontoDeOnibus>> ListarPontosAsync()
        {
            return await _contexto.PontosDeOnibus.ToListAsync();
        }

        public async Task<PontoDeOnibus> ObterPontoPorIdAsync(int id)
        {
            return await _contexto.PontosDeOnibus
                        .Where(p => p.Id == id)
                        .FirstOrDefaultAsync();
        }
    }
}
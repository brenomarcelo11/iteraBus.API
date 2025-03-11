using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;
using iteraBus.Repositorio.Inteface;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio
{
    public class LocalizacaoRepositorio : BaseRepositorio, ILocalizacaoRepositorio
    {
        public LocalizacaoRepositorio(IteraBusContexto contexto) : base(contexto)
        {}

        public async Task<int> AdicionarLocalizacaoAsync(Localizacao localizacao)
        {
            _contexto.Localizacoes.Add(localizacao);
            await _contexto.SaveChangesAsync();
            return localizacao.Id;
        }

        public async Task EditarLocalizacaoAsync(Localizacao localizacao)
        {
            _contexto.Localizacoes.Update(localizacao);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Localizacao>> ListarLocalizacoesAsync()
        {
            return await _contexto.Localizacoes.ToListAsync();
        }

        public async Task<Localizacao> ObterLocalizacaoPorIdAsync(int localizacaoId)
        {
            return await _contexto.Localizacoes
                        .Where(l => l.Id == localizacaoId)
                        .FirstOrDefaultAsync();
        }

        public async Task ExcluirLocalizacaoAsync(int localizacaoId)
        {
            var localizacaoExcluida = await _contexto.Localizacoes.FindAsync(localizacaoId);
            _contexto.Localizacoes.Remove(localizacaoExcluida);
            await _contexto.SaveChangesAsync();
        }
    }
}
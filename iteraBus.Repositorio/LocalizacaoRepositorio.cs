using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio
{
    public class LocalizacaoRepositorio : BaseRepositorio
    {
        public LocalizacaoRepositorio(IteraBusContexto contexto) : base(contexto)
        { }

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
    }

}
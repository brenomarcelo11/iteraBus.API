using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;

namespace iteraBus.Repositorio 
{
    public class LocalizacaoRepositorio : BaseRepositorio
    {
        public LocalizacaoRepositorio(IteraBusContexto contexto) : base(contexto)
        {} 
        
        public async Task<int> AdicionarLocalizacaoAsync(Localizacao localizacao)
        {
            _contexto.Localizacoes.Add(localizacao);
            await _contexto.SaveChangesAsync();
            return localizacao.Id;
        }

    }

}
using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Inteface;

namespace iteraBus.Aplicacao
{
    public class LocalizacaoAplicacao : ILocalizacaoAplicacao
    {
        readonly ILocalizacaoRepositorio _localizacaoRepositorio;
        public LocalizacaoAplicacao(ILocalizacaoRepositorio localizacaoRepositorio)
        {
            _localizacaoRepositorio = localizacaoRepositorio;
        }

        public async Task<int> AdicionarLocalizacaoAsync(Localizacao localizacao)
        {
            if (localizacao == null)
            {
                throw new Exception("Onibus não pode ser vazio.");
            }

            return await _localizacaoRepositorio.AdicionarLocalizacaoAsync(localizacao);
        }

        public async Task EditarLocalizacaoAsync(Localizacao localizacao)
        {
            var localizacaoDominio = await _localizacaoRepositorio.ObterLocalizacaoPorIdAsync(localizacao.Id);
            if (localizacaoDominio == null)
            {
                throw new Exception("Localização não encontrada.");
            }

            localizacaoDominio.OnibusId = localizacao.OnibusId;
            localizacaoDominio.Latitude = localizacao.Latitude;
            localizacaoDominio.Longitude = localizacao.Longitude;
            localizacaoDominio.Horario = localizacao.Horario;

            await _localizacaoRepositorio.EditarLocalizacaoAsync(localizacaoDominio);
        }

        public async Task<IEnumerable<Localizacao>> ListarLocalizacaoAsync()
        {
            return await _localizacaoRepositorio.ListarLocalizacoesAsync();
        }

        public async Task<Localizacao> ObterLocalizacaoPorIdAsync(int localizacaoId)
        {
            var localizacaoDominio = await _localizacaoRepositorio.ObterLocalizacaoPorIdAsync(localizacaoId);

            if  (localizacaoDominio == null)
            {
                throw new Exception("Localização não encontrada.");
            }

            return localizacaoDominio;
        }

        public async Task ExcluirLocalizacaoAsync(int localizacaoId)
        {
            var localizacaoDominio = await _localizacaoRepositorio.ObterLocalizacaoPorIdAsync(localizacaoId);

            if (localizacaoDominio == null)
            {
                throw new Exception("Localização não encontrada.");
            }

            await _localizacaoRepositorio.ExcluirLocalizacaoAsync(localizacaoId);
        }
    }
}
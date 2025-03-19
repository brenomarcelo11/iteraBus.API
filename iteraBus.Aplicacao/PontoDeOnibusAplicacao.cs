using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using iteraBus.Dominio.Repositorios;
using iteraBus.Repositorio.Inteface;

namespace iteraBus.Aplicacao
{
    public class PontoDeOnibusAplicacao : IPontoDeOnibusAplicacao
    {
        readonly IPontoDeOnibusRepositorio _pontoDeOnibusRepositorio;
        public PontoDeOnibusAplicacao(IPontoDeOnibusRepositorio pontoDeOnibusRepositorio)
        {
            _pontoDeOnibusRepositorio = pontoDeOnibusRepositorio;
        }

        public async Task<int> AdicionarPontoAsync(PontoDeOnibus ponto)
        {
            if (ponto == null)
            {
                throw new Exception("Ponto de ônibus não pode ser vazio.");
            }

            return await _pontoDeOnibusRepositorio.AdicionarPontoAsync(ponto);
        }

        public async Task EditarPontoAsync(PontoDeOnibus ponto)
        {
            var pontoDominio = await _pontoDeOnibusRepositorio.ObterPontoPorIdAsync(ponto.Id);
            if (pontoDominio == null)
            {
                throw new Exception("Ponto de ônibus não encontrada.");
            }

            pontoDominio.Nome = ponto.Nome;
            pontoDominio.Latitude = ponto.Latitude;
            pontoDominio.Longitude = ponto.Longitude;

            await _pontoDeOnibusRepositorio.AtualizarPontoAsync(pontoDominio);
        }

        public async Task<IEnumerable<PontoDeOnibus>> ListarPontosAsync()
        {
            return await _pontoDeOnibusRepositorio.ListarPontosAsync();
        }

        public async Task<PontoDeOnibus> ObterPontoPorIdAsync(int pontoId)
        {
            var pontoDominio = await _pontoDeOnibusRepositorio.ObterPontoPorIdAsync(pontoId);

            if  (pontoDominio == null)
            {
                throw new Exception("Ponto de ônibus não encontrado.");
            }

            return pontoDominio;
        }

        public async Task ExcluirPontoAsync(int pontoId)
        {
            var pontoDominio = await _pontoDeOnibusRepositorio.ObterPontoPorIdAsync(pontoId);

            if (pontoDominio == null)
            {
                throw new Exception("Ponto de ônibus não encontrado.");
            }

            await _pontoDeOnibusRepositorio.ExcluirPontoAsync(pontoId);
        }
    }
}
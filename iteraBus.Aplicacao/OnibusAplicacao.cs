using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Inteface;

namespace iteraBus.Aplicacao
{
    public class OnibusAplicacao : IOnibusAplicacao
    {
        readonly IOnibusRepositorio _onibusRepositorio;
        public OnibusAplicacao(IOnibusRepositorio onibusRepositorio)
        {
            _onibusRepositorio = onibusRepositorio;
        }

        public async Task<int> AdicionarOnibusAsync(Onibus onibus)
        {
            if (onibus == null)
            {
                throw new Exception("Onibus não pode ser vazio.");
            }

            return await _onibusRepositorio.AdicionarOnibusAsync(onibus);
        }

        public async Task EditarOnibusAsync(Onibus onibus)
        {
            var onibusDominio = await _onibusRepositorio.ObterOnibusPorIdAsync(onibus.Id);
            if (onibusDominio == null)
            {
                throw new Exception("Onibus não encontrado.");
            }

            onibusDominio.Placa = onibus.Placa;
            onibusDominio.Rota = onibus.Rota;

            await _onibusRepositorio.EditarOnibusAsync(onibusDominio);
        }

        public async Task<IEnumerable<Onibus>> ListarOnibusAsync()
        {
            return await _onibusRepositorio.ListarOnibusAsync();
        }

        public async Task<Onibus> ObterOnibusPorIdAsync(int onibusId)
        {
            var onibusDominio = await _onibusRepositorio.ObterOnibusPorIdAsync(onibusId);
            if (onibusDominio == null)
            {
                throw new Exception("Onibus não encontrado.");
            }

            return onibusDominio;
        }
    }
}
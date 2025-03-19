using iteraBus.Aplicacao.Interfaces;
using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Inteface;

namespace iteraBus.Aplicacao
{
    public class RotaAplicacao : IRotaAplicacao
    {
        readonly IRotaRepositorio _rotaRepositorio;
        public RotaAplicacao(IRotaRepositorio rotaRepositorio)
        {
            _rotaRepositorio = rotaRepositorio;
        }

        public async Task<int> AdicionarRotaAsync(Rota rota)
        {
            if (rota == null)
            {
                throw new Exception("Rota não pode ser vazia");
            }

            return await _rotaRepositorio.AdicionarRotaAsync(rota);
        }

        public async Task EditarRotaAsync(Rota rota)
        {
            var rotaDominio = await _rotaRepositorio.ObterRotaPorIdAsync(rota.Id);

            if (rotaDominio == null)
            {
                throw new Exception("Rota não encontrada.");
            }

            if (rota.Onibus != null && rota.Onibus.Count > 0)
            {
                foreach (var onibus in rota.Onibus)
                {
                    if (!rotaDominio.Onibus.Any(o => o.Id == onibus.Id))
                    {
                        rotaDominio.Onibus.Add(onibus);
                        onibus.RotaId = rota.Id;
                    }
                }
            }

            rotaDominio.Nome = rota.Nome;

            await _rotaRepositorio.EditarRotaAsync(rotaDominio);
        }

        public async Task<IEnumerable<Rota>> ListarRotasAsync()
        {
            return await _rotaRepositorio.ListarRotasAsync();
        }

        public async Task<Rota> ObterRotaPorIdAsync(int rotaId)
        {
            var rotaDominio = await _rotaRepositorio.ObterRotaPorIdAsync(rotaId);

            if (rotaDominio == null)
            {
                throw new Exception("Rota não encontrada.");
            }

            return rotaDominio;
        }

        public async Task ExcluirRotaAsync(int rotaId)
        {
            var rotaDominio = await _rotaRepositorio.ObterRotaPorIdAsync(rotaId);

            if (rotaDominio == null)
            {
                throw new Exception("Rota não encontrada");
            }

            await _rotaRepositorio.ExcluirRotaAsync(rotaId);
        }
    }
}

using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Inteface;

namespace iteraBus.Aplicacao
{
    public class OnibusAplicacao
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
    }
}
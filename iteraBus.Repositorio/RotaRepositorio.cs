using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;

namespace iteraBus.Repositorio
{
    public class RotaRepositorio : BaseRepositorio
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
            await _contexto.SaveChangesAsync();
        }

    }
}
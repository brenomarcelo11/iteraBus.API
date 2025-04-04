using iteraBus.Dominio.Entidades;
using iteraBus.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;

namespace iteraBus.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(IteraBusContexto contexto) : base(contexto)
        {
        }

        public async Task<int> CriarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return usuario.Id;
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }
        public async Task<Usuario> ObterPorIdAsync(int usuarioId)
        {
            return await _contexto.Usuarios
                .Where(u => u.Id == usuarioId)
                .FirstOrDefaultAsync();
        }
        public async Task<Usuario> ObterDesativoAsync(int usuarioId)
        {
            return await _contexto.Usuarios
                .Where(u => u.Id == usuarioId)
                .Where(u => u.Ativo == false)
                .FirstOrDefaultAsync();
        }
        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _contexto.Usuarios
                .Where(u => u.Email == email)
                .Where(u => u.Ativo)
                .FirstOrDefaultAsync();
        }
        public async Task<Usuario> ObterPorTokenAsync(string token)
        {
            return await _contexto.Usuarios
                .Where(u => u.TokenRecuperacao == token)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Usuario>> ListarAsync(bool ativo)
        {
            return await _contexto.Usuarios
                .Where(u => u.Ativo == ativo)
                .ToListAsync();
        }

        public async Task FavoritarRotaAsync(int usuarioId, int rotaId)
        {
            var usuario = await ObterPorIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            var rota = await _contexto.Rotas.FindAsync(rotaId);
            if (rota == null)
                throw new Exception("Rota não encontrada.");

            if (!usuario.RotasFavoritas.Contains(rota))
            {
                usuario.RotasFavoritas.Add(rota);
                await _contexto.SaveChangesAsync();
            }
        }

        // 📌 Método para desfavoritar uma rota
        public async Task DesfavoritarRotaAsync(int usuarioId, int rotaId)
        {
            var usuario = await ObterPorIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            var rota = usuario.RotasFavoritas.FirstOrDefault(r => r.Id == rotaId);
            if (rota != null)
            {
                usuario.RotasFavoritas.Remove(rota);
                await _contexto.SaveChangesAsync();
            }
        }
    }

}
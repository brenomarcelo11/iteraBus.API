namespace iteraBus.Api.Models
{
    public class UsuarioAtualizar
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}
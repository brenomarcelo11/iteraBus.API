namespace iteraBus.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public int TipoUsuarioId { get; set; }
        public string TokenRecuperacao { get; set; }
        public DateTime? TokenExpiracao { get; set; }
        public List<Rota> RotasFavoritas { get; set; } = new List<Rota>();

        public Usuario()
        {
            Ativo = true;
        }

        public void Deletar()
        {
            Ativo = false;
        }
        public void Restaurar()
        {
            Ativo = true;
        }
    }
}
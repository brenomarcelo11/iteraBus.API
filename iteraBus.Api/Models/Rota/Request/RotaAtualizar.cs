namespace iteraBus.Api.Models
{
    public class RotaAtualizar
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<int> OnibusIds { get; set; }
        public List<int> PontosDeOnibusIds { get; set; }
    }
}

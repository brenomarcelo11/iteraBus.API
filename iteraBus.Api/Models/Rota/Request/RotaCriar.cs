namespace iteraBus.Api.Models
{
    public class RotaCriar
    {
        public string Nome { get; set; }
        public List<int> OnibusIds { get; set; }
        public List<int> PontosDeOnibusIds { get; set; }
    }
}

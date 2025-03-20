namespace iteraBus.Api.Models 
{
    public class PontoDeOnibusResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RotaId { get; set; }
    }
}
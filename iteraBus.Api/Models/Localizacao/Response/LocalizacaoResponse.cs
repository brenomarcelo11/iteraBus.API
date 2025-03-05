namespace iteraBus.Api.Models
{
    public class LocalizacaoResponse
    {
        public int Id { get; set; }
        public int OnibusId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Horario { get; set; }
    }
}

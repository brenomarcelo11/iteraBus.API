namespace iteraBus.Dominio.Entidades
{
    public class Localizacao
    {
        public int Id { get; set; }
        public int OnibusId { get; set; }
        public Onibus Onibus { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Horario { get; set; }

        public Localizacao()
        {
            Horario = DateTime.Now;
        }
    }
}
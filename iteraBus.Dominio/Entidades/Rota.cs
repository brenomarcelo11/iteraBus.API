namespace iteraBus.Dominio.Entidades
{
    public class Rota
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Onibus> Onibus { get; set; } = new List<Onibus>();
        public List<PontoDeOnibus> PontosDeOnibus { get; set; } = new List<PontoDeOnibus>();
    }
}
namespace iteraBus.Dominio.Entidades
{
    public class Rota
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Onibus> Onibus { get; set; }
        public List<PontoDeOnibus> PontosDeOnibus { get; set; }

        public Rota()
        {
            Onibus = new List<Onibus>();
            PontosDeOnibus = new List<PontoDeOnibus>();
        }
    }
}
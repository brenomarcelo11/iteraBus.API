namespace iteraBus.Dominio.Entidades 
{
    public class Onibus
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Linha { get; set; }
        public int RotaId { get; set; }
        public Rota Rota { get; set; }
        public List<Localizacao> Localizacoes { get; set; }

        public Onibus()
        {
            Localizacoes = new List<Localizacao>();
        }
    }
}
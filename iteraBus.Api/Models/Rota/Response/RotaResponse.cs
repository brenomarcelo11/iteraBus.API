namespace iteraBus.Api.Models
{
    public class RotaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<int> OnibusIds { get; set; }
        public List<PontoDeOnibusResponse> PontosDeOnibus { get; set; }

        public RotaResponse()
        {
            OnibusIds = new List<int>();
            PontosDeOnibus = new List<PontoDeOnibusResponse>();
        }
    }
}

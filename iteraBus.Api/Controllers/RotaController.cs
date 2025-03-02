using iteraBus.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly IRotaAplicacao _rotaAplicacao;
        public RotaController(IRotaAplicacao rotaAplicacao)
        {
            _rotaAplicacao = rotaAplicacao;
        }
    }
}
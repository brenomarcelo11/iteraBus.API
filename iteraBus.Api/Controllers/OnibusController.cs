using iteraBus.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iteraBus.Api.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class OnibusController : ControllerBase
    {
       private readonly IOnibusAplicacao _onibusAplicacao;


    }
}
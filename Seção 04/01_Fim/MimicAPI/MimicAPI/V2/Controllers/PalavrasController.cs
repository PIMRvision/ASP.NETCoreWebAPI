using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.V2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")] //api/palavras?api-version=2.0
    /// <summary>
    /// Operação que obtém do banco de dados, todas as palavras existentes.
    /// </summary>
    /// <param name="query">Filtros para pesquisa.</param>
    /// <returns>Listagem de Palavras.</returns>
    [ApiVersion("2.0")]
    public class PalavrasController : ControllerBase
    {
        [HttpGet("", Name = "ObterTodas")]
        public string ObterTodas()
        {
            return "Versão 2.0";
        }
    }
}

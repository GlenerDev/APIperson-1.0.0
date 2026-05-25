
using Microsoft.AspNetCore.Mvc;
using teste.Models;
using teste.Models.NovaPasta1.VO;
namespace teste.Controllers
{

    [ApiController]/* que essa classe é uma apicontroller e sem ela não poderia fazer as validaçoes ou a verificação 
    da requisições */
    [Route("v1/Person")]

    public class PersonController : ControllerBase
    {
        public Dbase.DBase DataBase { get; set; }
        public PersonController()
        {
            DataBase = new Dbase.DBase();
        }
        [HttpGet("GetPerson/id/{id}")]
        public IActionResult Get(int id)
        {
            return Ok("foi um cusseo");
        }
        [HttpGet("GetVerifyConnection")]
        public IActionResult VerifyConnection()
        {
            if (DataBase.Connect())
            {
                return Ok("conectado com Sucesso!");
            }
            return Problem("Falha na conexão");
        }
    }
}

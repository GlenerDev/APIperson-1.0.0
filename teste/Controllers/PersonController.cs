using Microsoft.AspNetCore.Mvc;
using APIperson.Models.DTOs;
using teste.Models;
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
        public ActionResult<List<Person>> Get(int id)
        {
            return DataBase.GetAllUsers();
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
        [HttpGet("GetAllUsers")]
        public IEnumerable<Person> Get()
        {
            return DataBase.GetAllUsers();
        }
        [HttpPost("SetPerson")]
        public ActionResult SetPerson([FromBody] Person person) 
        {
            return Ok(person);
        }
    }
}

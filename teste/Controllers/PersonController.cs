using Microsoft.AspNetCore.Mvc;
using APIperson.Models.DTOs;
using APIperson.Models;
using Repository;
using System.Data.SQLite;
using APIperson.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
namespace APIperson.Controllers
{

    [ApiController]/* que essa classe é uma apicontroller e sem ela não poderia fazer as validaçoes ou a verificação 
    da requisições */
    [Route("v1/Person")]
    public class PersonController : ControllerBase
    {
       public DBase DB { get; set; }
        public PersonController(DBase dbase)
        {
            DB = dbase;
        }
        [HttpGet("{id}")]
        public IActionResult GetPersonID(int id)
        {
            var ps = DB.GetIDPerson(id);
            if (ps is null)
                return NotFound("Recurso solicitado não encontrado");
            return Ok();
        }
        [HttpGet("GetVerifyConnection")]
        public IActionResult VerifyConnection()
        {
            if (DB.VerifyOpenConnect())
            {
                return Ok("O Armazenamento esta disponivel");
            }
            return StatusCode(503, "Conexão não disponivel");
        }
        [HttpGet("GetAllUsers")]
        public IEnumerable<Person> GetAll() => DB.GelAllUsers();
        [HttpPost("SetPerson")]
        public ActionResult SetPerson([FromBody] DTOPerson person)
        {
            if (person is null)
            {
                return BadRequest("Erro no servidor ao adicionar a pessoa");
            }
            DB.CreatePerson(person.Name, person.Idade, person.CPF);
            return StatusCode(201, "usuario adicionado com sucesso");
        }

        [HttpDelete("DeleteForId")]
        public IActionResult DeleteForID(int id)
        {
            DB.DeleteForId(id);
            return Ok("recurso deletado com sucesso");
        }
    }
}

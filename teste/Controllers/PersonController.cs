using Microsoft.AspNetCore.Mvc;
using APIperson.Models.DTOs;
using APIperson.Models;
using Repository;
using System.Data.SQLite;
using APIperson.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using APIperson.Services;
using APIperson._Services;
using System.Runtime.InteropServices.Marshalling;
namespace APIperson.Controllers
{

    [ApiController]/* que essa classe é uma apicontroller e sem ela não poderia fazer as validaçoes ou a verificação 
    da requisições */
    [Route("v1/Person")]
    public class PersonController : ControllerBase
    {
        private readonly RepositoryServices _repositoryServices;
        private readonly PersonServices _personServices;

        public PersonController(PersonServices personservice, RepositoryServices repositoryservice)
        {
            _personServices = personservice;
            _repositoryServices = repositoryservice;
        }

        [HttpGet]
        public IActionResult GetPersonID([Required] int id)
        {
            Person pessoa = _personServices.ServiceGetIDPerson(id);
            if (pessoa.Id == 0) return NotFound("Recurso solicitado não encontrado");
            return Ok(new DTOPerson(pessoa.Id, pessoa.Name, pessoa.Age));
        }
        [HttpGet("GetVerifyConnection")]
        public ActionResult VerifyConnection()
        {
            try
            {
                if (_repositoryServices.VerifyOpenConnect())
                {
                    return Ok("conexão disponivel.");
                }
                return StatusCode(503);
            }
            catch (Exception ex) { return StatusCode(503, ex.Message); }
        }
        [HttpGet("GetAllUsers")]
        public ActionResult GetAllPersons()
        {
            try
            {
                if (_personServices.ServiceGetAllUsers() == null) { StatusCode(404, "Nenhum registro presente no momento"); }
                return Ok(_personServices.ServiceGetAllUsers());
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("SetPerson")]
        public ActionResult SetPerson([FromBody][Required] Person person)
        {
            try
            {
                _personServices.ServiceCreatePerson(person);
                return StatusCode(201, person);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("DeleteForId")]
        public IActionResult DeleteForID([Required] int id)
        {
            try
            {
                _personServices.ServiceDeleteForId(id);
                return Ok("recurso deletado com sucesso");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpPut("UpdatePersonId")]
        public ActionResult UpdatePersonId([FromBody][Required] Person p)
        {
            try
            {
                _personServices.ServiceUpdatePersonId(p.Id,p.Name,p.Age); 
                Ok(p);
                return Ok("Pessoa com campo atualizados");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}

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
using System.Collections;
namespace APIperson.Controllers
{

    [ApiController]/* que essa classe é uma apicontroller e sem ela não poderia fazer as validaçoes ou a verificação 
    da requisições */
    [Route("v1/")]
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
        public async Task<IActionResult> GetPersonID([Required] int id)
        {
            Person pessoa = _personServices.ServiceGetIDPerson(id);
            if (pessoa.Id == 0) return NotFound("Recurso solicitado não encontrado");
            return Ok(new DTOPerson(pessoa.Id, pessoa.Name, pessoa.Age));
        }
        [HttpGet("GetVerifyConnection")]
        public async Task<ActionResult> VerifyConnection()
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
        public async Task <ActionResult> GetAllPersons()
        {
            try
            {
                if (_personServices.ServiceGetAllUsers() == null) 
                { 
                    StatusCode(404, "Nenhum registro presente no momento"); 
                }
                return Ok(_personServices.ServiceGetAllUsers());
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("SetPerson")]
        public async Task<ActionResult> SetPerson([FromBody][Required] DTOCreatePerson person)
        {
            try
            {
                _personServices.ServiceCreatePerson(new Person { Name = person.Nome, Age = person.Idade, CPF = person.CPF });
                return StatusCode(201, person);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("DeleteForId")]
        public async Task<IActionResult> DeleteForID([Required] int id)
        {
            try
            {
                await _personServices.ServiceDeleteForId(id);
                return Ok("recurso deletado com sucesso");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
        [HttpPut("UpdatePersonId")]
        public async Task<ActionResult> UpdatePersonId([Required] int id,[FromBody][Required] DTOCreatePerson p)
        {
            try
            {
                _personServices.ServiceUpdatePersonId(id, p.Nome, p.Idade,p.CPF);
                Ok(p);
                return Ok("Pessoa com campo atualizados");
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}

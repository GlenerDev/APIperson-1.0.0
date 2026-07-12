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

        [HttpGet("Get/id")]
        public async Task<IActionResult> GetPersonID([Required] int id)
        {
            try
            {
                Person pessoa = await _personServices.ServiceGetIDPerson(id);
                return Ok(new DTOPerson(pessoa.ID, pessoa.Nome, pessoa.Idade));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }
        [HttpGet("Get/VerifyConnection")]
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
        [HttpGet("Get/AllUsers")]
        public async Task<ActionResult> GetAllPersons()
        {
            List<DTOPerson> listresulthtpp = new List<DTOPerson>();
            try
            {
                _personServices.ServiceGetAllUsers().Result.ForEach(x => listresulthtpp.Add(new DTOPerson(x.ID, x.Nome, x.Idade)));
                return Ok(listresulthtpp);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("SetPerson")]
        public async Task<ActionResult> SetPerson([FromBody][Required] DTOCreatePerson person)
        {
            try
            {
                await _personServices.ServiceCreatePerson(new Person { Nome = person.Nome, Idade = person.Idade, CPF = person.CPF });
                return Ok("Recurso criado com sucesso.");
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
            catch (Exception ex) { return NotFound(ex.Message); }
        }
        [HttpPut("UpdatePersonId")]
        public async Task<ActionResult> UpdatePersonId([Required] int id, [FromBody][Required] DTOCreatePerson p)
        {
            try
            {
                await _personServices.ServiceUpdatePersonId(id, p.Nome, p.Idade, p.CPF);
                return Ok("Pessoa com campo atualizados");
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }
    }
} 


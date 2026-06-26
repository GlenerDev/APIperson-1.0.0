using APIperson._Services.PersonValidations;
using APIperson.Interfaces;
using APIperson.Models;
using APIperson.Models.DTOs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Repository;
using System.Data.SQLite;
using System.Globalization;
using System.Text.RegularExpressions;

namespace APIperson.Services
{
    public class PersonServices : ValidationPerson , IPersonRepository
    {
        private readonly DBase DB;

        public PersonServices(DBase dBase,ValidationPerson valitadionperson)
        {
            DB = dBase;
            _validation = valitadionperson;
        }
        
        public void CreatePerson(string name, int age, string CPF)
        {
            if ( _validation.ValidationCreatePerson(name, age, CPF))
            {
                 DB.CreatePerson(name,age,CPF);
                return;
            }
            throw new ArgumentException("erro na declaraçao do solicitado.");
        }
        public Person GetIDPerson(int id)
        {
            var person = DB.GetIDPerson(id);
            if (person == null)
            {
                throw new KeyNotFoundException("Possoa não existe no banco de dados");
            }
            return person;
        }
        public List<DTOPerson> GetAllUsers() 
        {
            if (DB.GelAllUsers().Count == 0) 
            {
                throw new InvalidOperationException("nenhuma pessoa encontrada");
            }
            return DB.GelAllUsers();
        }
        public void DeleteForId(int id) 
        {
        
        
        }
        public void UpdatePersonId(int id, string name, int age, string cpf)
        {
            DB.
        }
    }
}

using APIperson._Services.PersonValidations;
using APIperson.Interfaces;
using APIperson.Models;
using APIperson.Models.DTOs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Repository;
using APIperson.Services;
using System.Data.SQLite;
using System.Globalization;
using System.Text.RegularExpressions;

namespace APIperson.Services
{
    public class PersonServices : IPersonRepositoryService
    {
        private readonly DBase DB;
        private readonly ValidationPerson Valid;
        public PersonServices(DBase dbase, ValidationPerson validationperson)
        {
            DB = dbase;
            Valid = validationperson;
        }
        public void ServiceCreatePerson(Person person)
        {
            if (Valid.ValidationCreatePerson(person))
            {
                DB.CreatePerson(person);
                return;
            }
            throw new ArgumentException("erro na declaraçao do solicitado.");
        }
        public Person ServiceGetIDPerson(int id)
        {
            if (Valid.IdValidation(id))
            {
                if (DB.PersonExist(DB.GetIDPerson(id).Id))
                {
                    return DB.GetIDPerson(id);
                }
            }
            throw new ArgumentException("Possoa não existe no banco de dados");
        }
        public List<DTOPerson> ServiceGetAllUsers()
        {
            if (DB.GetAllUsers().Count != 0)
            {
                return DB.GetAllUsers();
            }
            throw new InvalidOperationException("nenhuma pessoa encontrada");
        }
        public void ServiceDeleteForId(int id)
        {
            if (DB.PersonExist(id) && Valid.IdValidation(id))
            {
                DB.DeleteForId(id);
                return;
            }
            throw new ArgumentException("não tem como deletar uma pessoa que não existe no banco");
        }
        public void ServiceUpdatePersonId(int id, string nome, int idade)
        {
            if (DB.PersonExist(id) && Valid.IdValidation(id))
            {
                DB.UpdatePersonId(id, nome, idade);
                return;
            }
            throw new ArgumentException("Nao tem como atualizar uma pessoa que não existe no banco");
        }
    }
}


